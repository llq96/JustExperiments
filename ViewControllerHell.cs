namespace ExamplesForInterview;

public class ViewControllerHell
{
    public void Run()
    {
        var view = new IdleView();
        var controller = new IdleController();
        var state = new State<IdleView, IdleController>(view, controller);

        var a = view.View;
        var b = controller.Controller;
        Console.WriteLine(state.View.Controller);
        Console.WriteLine(state.Controller.View);
    }

    public abstract class State(BaseStatePart view, BaseStatePart controller)
    {
        public BaseStatePart View = view;
        public BaseStatePart Controller = controller;
    }

    public class State<TView, TController> : State
        where TView : BasePartView<TView, TController>
        where TController : BasePartController<TView, TController>
    {
        public TView View;
        public TController Controller;

        public State(TView view, TController controller) : base(view, controller)
        {
            View = view;
            Controller = controller;

            View.State = this;
            Controller.State = this;
        }
    }

    public class BaseStatePart
    {
    }

    public class StatePart<TView, TController> : BaseStatePart
        where TView : class
        where TController : class
    {
        public State State;
        public TView View => State.View as TView;
        public TController Controller => State.Controller as TController;
    }

    public class BasePartView<TView, TController> : StatePart<TView, TController>
        where TView : class
        where TController : class
    {
    }

    public class BasePartController<TView, TController> : StatePart<TView, TController>
        where TView : class
        where TController : class
    {
    }

    public class IdleView : BasePartView<IdleView, IdleController>
    {
    }

    public class IdleController : BasePartController<IdleView, IdleController>
    {
    }

    //----------------------------------------------------------------------------------

    // public class State<TView, TController>
    //     where TView : BasePartView<TView, TController>
    //     where TController : BasePartController<TView, TController>
    // {
    //     public TView View;
    //     public TController Controller;
    //
    //     public State(TView view, TController controller)
    //     {
    //         View = view;
    //         Controller = controller;
    //
    //         View.State = this;
    //         Controller.State = this;
    //     }
    // }
    //
    // public class StatePart<TView, TController>
    //     where TView : BasePartView<TView, TController>
    //     where TController : BasePartController<TView, TController>
    // {
    //     public State<TView, TController> State;
    // }
    //
    // public class BasePartView<TView, TController> : StatePart<TView, TController>
    //     where TView : BasePartView<TView, TController>
    //     where TController : BasePartController<TView, TController>
    // {
    //     public TController Controller => State.Controller;
    // }
    //
    // public class BasePartController<TView, TController> : StatePart<TView, TController>
    //     where TView : BasePartView<TView, TController>
    //     where TController : BasePartController<TView, TController>
    // {
    //     public TView View => State.View;
    // }
    //
    // public class IdleView : BasePartView<IdleView, IdleController>
    // {
    // }
    //
    // public class IdleController : BasePartController<IdleView, IdleController>
    // {
    // }


    //----------------------------------------------------------------------------------

    // public class State<TView, TController>
    //     where TView : BasePartView<TView, TController>
    //     where TController : BasePartController<TView, TController>
    // {
    //     public TView View { get; }
    //     public TController Controller { get; }
    //
    //     public State(TView view, TController controller)
    //     {
    //         View = view;
    //         Controller = controller;
    //
    //         view.Controller = Controller;
    //         controller.View = view;
    //     }
    // }
    //
    // public abstract class BasePart<TView, TController>
    // {
    //     // public State<TView, TController> State { get; set; }
    // }
    //
    // public abstract class BasePartView<TView, TController> : BasePart<TView, TController>
    // {
    //     public TController Controller;
    // }
    //
    // public abstract class BasePartController<TView, TController> : BasePart<TView, TController>
    // {
    //     public TView View;
    // }
    //
    // public class IdleView : BasePartView<IdleView, IdleController>
    // {
    // }
    //
    // public class IdleController : BasePartController<IdleView, IdleController>
    // {
    // }
}