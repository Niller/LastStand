using Assets.src.models;

namespace Assets.src.mediators {
    public interface IViewModelMediator {
        //IModel GetModel();
        T1 GetModel<T1>() where T1 : class, IModel;
    }
}