﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DotMake.CommandLine.SourceGeneration {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DotMake.CommandLine.SourceGeneration.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Circular parent dependency involving the classes &apos;{0}&apos; and &apos;{1}&apos;. To fix this error, break the circular dependency by removing one of the attribute argument Parent..
        /// </summary>
        internal static string ClassCircularDependency {
            get {
                return ResourceManager.GetString("ClassCircularDependency", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The class &apos;{0}&apos; will not have a {1} (ignored) because it does not have a method with one of the following signatures: &apos;void Run()&apos;, &apos;int Run()&apos;, &apos;async Task RunAsync()&apos;, &apos;async Task&lt;int&gt; RunAsync()&apos;. Optionally the method signature can have an InvocationContext parameter: &apos;Run(InvocationContext context)&apos;, &apos;RunAsync(InvocationContext context)&apos;.
        /// </summary>
        internal static string ClassHasNotHandler {
            get {
                return ResourceManager.GetString("ClassHasNotHandler", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The class &apos;{0}&apos; must have a parameterless (default) and public/internal and non-static constructor to generate a {1} when not using dependency injection..
        /// </summary>
        internal static string ClassHasNotPublicDefaultConstructor {
            get {
                return ResourceManager.GetString("ClassHasNotPublicDefaultConstructor", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The class &apos;{0}&apos; must be a  non-generic and non-abstract class to generate a {1}..
        /// </summary>
        internal static string ClassNotNonAbstractNonGeneric {
            get {
                return ResourceManager.GetString("ClassNotNonAbstractNonGeneric", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The class &apos;{0}&apos; will not generate a {1} (ignored) because it is not a public/internal and non-static class..
        /// </summary>
        internal static string ClassNotPublicNonStatic {
            get {
                return ResourceManager.GetString("ClassNotPublicNonStatic", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The command as delegate must have a return type &apos;void&apos; or &apos;int&apos; and if it&apos;s async &apos;Task&apos; or &apos;Task&lt;int&gt;&apos;..
        /// </summary>
        internal static string DelegateNotCorrect {
            get {
                return ResourceManager.GetString("DelegateNotCorrect", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The method &apos;{0}&apos; must be a  non-generic method to generate a {1}..
        /// </summary>
        internal static string MethodNotNonGeneric {
            get {
                return ResourceManager.GetString("MethodNotNonGeneric", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The method &apos;{0}&apos; will not generate a {1} (ignored) because it is not a public/internal and non-static method..
        /// </summary>
        internal static string MethodNotPublicNonStatic {
            get {
                return ResourceManager.GetString("MethodNotPublicNonStatic", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The class &apos;{0}&apos; must have its &apos;Parent&apos; attribute argument point to a class which has {2}, to generate a {1}..
        /// </summary>
        internal static string ParentClassHasNotAttribute {
            get {
                return ResourceManager.GetString("ParentClassHasNotAttribute", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The property &apos;{0}&apos; must have a public/internal get accessor to generate a {1}..
        /// </summary>
        internal static string PropertyHasNotPublicGetter {
            get {
                return ResourceManager.GetString("PropertyHasNotPublicGetter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The property &apos;{0}&apos; must have a public/internal set accessor to generate a {1}..
        /// </summary>
        internal static string PropertyHasNotPublicSetter {
            get {
                return ResourceManager.GetString("PropertyHasNotPublicSetter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The property &apos;{0}&apos; will not generate a {1} (ignored) because it is not a public/internal and non-static property..
        /// </summary>
        internal static string PropertyNotPublicNonStatic {
            get {
                return ResourceManager.GetString("PropertyNotPublicNonStatic", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The property &apos;{0}&apos; has custom enumerable type &apos;{2}&apos; (other than supported CLR types) so it should have a a public constructor with a IEnumerable&lt;T&gt; or IList&lt;T&gt; parameter (other parameters, if any, should be optional), so that it can be bound/parsed as a {1} ..
        /// </summary>
        internal static string PropertyTypeEnumerableIsNotBindable {
            get {
                return ResourceManager.GetString("PropertyTypeEnumerableIsNotBindable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The property &apos;{0}&apos; has custom type &apos;{2}&apos; (other than supported CLR types) so it should have a public constructor or a static &apos;Parse&apos; method with a string parameter (other parameters, if any, should be optional), so that it can be bound/parsed as a {1} ..
        /// </summary>
        internal static string PropertyTypeIsNotBindable {
            get {
                return ResourceManager.GetString("PropertyTypeIsNotBindable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Source generation currently supports only C# language in the consuming project..
        /// </summary>
        internal static string UnsupportedLanguage {
            get {
                return ResourceManager.GetString("UnsupportedLanguage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Source generation requires at least C# version {0} in the consuming project. Please add &lt;LangVersion&gt;9.0&lt;/LangVersion&gt; tag to your .csproj file..
        /// </summary>
        internal static string UnsupportedLanguageVersion {
            get {
                return ResourceManager.GetString("UnsupportedLanguageVersion", resourceCulture);
            }
        }
    }
}
