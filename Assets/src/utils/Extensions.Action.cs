using System;

namespace Assets.Common.Extensions {
    public static partial class Extensions {
        public static void TryCall<T0, T1>(this Action<T0, T1> target, T0 param0, T1 param1) {
            var local = target;
            if (local != null) {
                local(param0, param1);
            }
        }

        public static void TryCall<T>(this Action<T> target, T param) {
            var local = target;
            if (local != null) {
                local(param);
            }
        }

        public static void TryCall(this Action target) {
            var local = target;
            if (local != null) {
                local();
            }
        }
    }
}
