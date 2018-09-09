using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Walterlv.ComponentModel
{
    /// <summary>
    /// 包含在运行时判断编译器编译配置中调试信息相关的属性。
    /// </summary>
    public static class DebuggingProperties
    {
        /// <summary>
        /// 检查当前正在运行的主程序是否是在 Debug 配置下编译生成的。
        /// </summary>
        public static bool IsDebug
        {
            get
            {
                if (_isDebugMode == null)
                {
                    var assembly = Assembly.GetEntryAssembly();
                    if (assembly == null)
                    {
                        // 由于调用 GetFrames 的 StackTrace 实例没有跳过任何帧，所以 GetFrames() 一定不为 null。
                        assembly = new StackTrace().GetFrames().Last().GetMethod().Module.Assembly;
                    }

                    var debuggableAttribute = assembly.GetCustomAttribute<DebuggableAttribute>();
                    _isDebugMode = debuggableAttribute.DebuggingFlags
                        .HasFlag(DebuggableAttribute.DebuggingModes.EnableEditAndContinue);
                }

                return _isDebugMode.Value;
            }
        }

        private static bool? _isDebugMode;
    }
}