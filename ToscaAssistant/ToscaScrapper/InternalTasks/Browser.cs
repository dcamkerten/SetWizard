using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ToscaScrapper.InternalTasks
{

    public class Browser : IDisposable
    {
        #region Fields

        private static TimeSpan _defaultWait = TimeSpan.FromSeconds(0);
        private readonly ManualResetEvent _signal;
        private WebBrowser _browser;
        private DateTime? _lastCompleted;
        private HeadlessForm _form;

        #endregion

        #region Constructors and Destructors
        public Browser()
            : this(false)
        {
        }

        public Browser(bool visibility)
        {
            _signal = new ManualResetEvent(false);

            var thread = new Thread(
                () => {
                    _browser = new WebBrowser
                    {
                        ScriptErrorsSuppressed = true
                    };

                    _browser.Navigating += (p, q) => _lastCompleted = null;
                    _browser.DocumentCompleted += (p, q) => _lastCompleted = DateTime.UtcNow;



                    _form = new HeadlessForm();

                    _form.Controls.Add(_browser);
                    _form.HandleCreated += (p, q) => _signal.Set();
                    _form.InitialVisibility = visibility;
                    _form.Visible = visibility;

                    Application.Run(_form);
                }
            );

            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();

            _signal.WaitOne();
        }

        #endregion

        #region Public Properties

        public virtual TimeSpan DefaultWait
        {
            get { return _defaultWait; }
            set { _defaultWait = value; }
        }

        #endregion

        #region Public Methods and Operators

        public virtual void Await(Action<WebBrowser> order)
        {
            Await(
                DefaultWait,
                order
            );
        }

        public virtual void Await(int wait, Action<WebBrowser> order)
        {
            Await(
                CreateWaitTimeSpan(wait),
                order
            );
        }

        public virtual void Await(TimeSpan wait, Action<WebBrowser> order)
        {
            Await(
                wait,
                new[] { order }
            );
        }

        public virtual void Await(params Action<WebBrowser>[] orders)
        {
            Await(
                DefaultWait,
                orders
            );
        }

        public virtual void Await(int wait, params Action<WebBrowser>[] orders)
        {
            Await(
                CreateWaitTimeSpan(wait),
                orders
            );
        }

        public virtual void Await(int[] waits, params Action<WebBrowser>[] orders)
        {
            Await(
                waits.Select(CreateWaitTimeSpan).ToArray(),
                orders
            );
        }

        public virtual void Await(TimeSpan wait, params Action<WebBrowser>[] orders)
        {
            Await(
                orders.Select(p => wait).ToArray(),
                orders
            );
        }

        public virtual void Await(TimeSpan[] waits, params Action<WebBrowser>[] orders)
        {
            Await(
                waits,
                orders.Select(
                    (p, q) => (Func<WebBrowser, object>)(r => { p(r); return null; })
                ).ToArray()
            );
        }


        public virtual T Await<T>(Func<WebBrowser, T> order)
        {
            return Await(
                DefaultWait,
                order
            );
        }

        public virtual T Await<T>(int wait, Func<WebBrowser, T> order)
        {
            return Await(
                CreateWaitTimeSpan(wait),
                order
            );
        }

        public virtual T Await<T>(TimeSpan wait, Func<WebBrowser, T> order)
        {
            return Await(
                wait,
                new[] { order }
            ).First();
        }

        public virtual T[] Await<T>(params Func<WebBrowser, T>[] orders)
        {
            return Await(
                DefaultWait,
                orders
            );
        }

        public virtual T[] Await<T>(int wait, params Func<WebBrowser, T>[] orders)
        {
            return Await(
                CreateWaitTimeSpan(wait),
                orders
            );
        }

        public virtual T[] Await<T>(int[] waits, params Func<WebBrowser, T>[] orders)
        {
            return Await(
                waits.Select(CreateWaitTimeSpan).ToArray(),
                orders
            );
        }

        public virtual T[] Await<T>(TimeSpan wait, params Func<WebBrowser, T>[] orders)
        {
            return Await(
                orders.Select(p => wait).ToArray(),
                orders
            );
        }

        public virtual T[] Await<T>(TimeSpan[] waits, params Func<WebBrowser, T>[] orders)
        {
            if (waits.Length != orders.Length)
                throw new ArgumentException("The waits and orders arguments must have the same length.");

            var results = new T[orders.Length];

            for (var i = 0; i < orders.Length; i++)
            {
                var i1 = i;
                _form.Execute(
                    () => results[i1] = orders[i1](_browser)
                );

                while (true)
                {
                    if (!_lastCompleted.HasValue)
                    {
                        Thread.Sleep(50);
                        continue;
                    }

                    var diff = _lastCompleted.Value.Add(waits[i]) - DateTime.UtcNow;

                    if (diff.Ticks < 0)
                        break;

                    Thread.Sleep(diff);
                }
            }

            return results;
        }

        public virtual void Dispose()
        {
            _form.Execute(
                () => _form.Close()
            );
        }

        #endregion

        #region Methods

        private TimeSpan CreateWaitTimeSpan(int wait)
        {
            return TimeSpan.FromMilliseconds(wait);
        }

        #endregion

        private sealed class HeadlessForm : Form
        {
            #region Public Properties

            public bool InitialVisibility { private get; set; }

            #endregion

            #region Public Methods and Operators

            public void Execute(Action action)
            {
                Invoke(action);
            }

            #endregion

            #region Methods

            protected override void SetVisibleCore(bool value)
            {
                if (!IsHandleCreated)
                    CreateHandle();

                base.SetVisibleCore(InitialVisibility);
            }

            #endregion
        }
    }
}