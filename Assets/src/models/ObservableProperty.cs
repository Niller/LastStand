using System;
using Assets.Common.Extensions;

namespace Assets.src.models {
    public class ObservableProperty<T> {
        public Action<T> OnPropertyChanged { get; set; }
        protected T propertyValue;
        public T Value  {
            get {
                return propertyValue;      
            }
            set {
                propertyValue = value;
                OnPropertyChanged.TryCall(propertyValue);
            }
            
        }

        public ObservableProperty(T value) {
            Value = value;
        } 
    }
}