using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;

namespace DotMake.CommandLine
{
    /// <summary>
    /// Represents a command builder generated by the source generator.
    /// </summary>
    public abstract class CliCommandBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CliCommandBuilder" /> class.
        /// </summary>
        protected CliCommandBuilder()
        {
            var defaults = CliCommandAttribute.Default;
            NameCasingConvention = defaults.NameCasingConvention;
            NamePrefixConvention = defaults.NamePrefixConvention;
            ShortFormPrefixConvention = defaults.ShortFormPrefixConvention;
            ShortFormAutoGenerate = defaults.ShortFormAutoGenerate;
        }

        /// <summary>
        /// The definition class that this command builder is generated for.
        /// </summary>
        public Type DefinitionType { get; protected set; }

        /// <summary>
        /// The parent definition class if this command builder is generated for a nested/external child.
        /// </summary>
        public Type ParentDefinitionType { get; protected set; }

        /// <summary>
        /// Gets the character casing convention used for automatically generated command, option and argument names.
        /// </summary>
        public CliNameCasingConvention NameCasingConvention { get; protected set; }

        /// <summary>
        /// Gets the prefix convention used for automatically generated option names.
        /// </summary>
        public CliNamePrefixConvention NamePrefixConvention { get; protected set; }

        /// <summary>
        /// Gets the prefix convention used for automatically generated short form option aliases.
        /// </summary>
        public CliNamePrefixConvention ShortFormPrefixConvention { get; protected set; }

        /// <summary>
        /// Gets a value which indicates whether short form aliases were automatically generated for options.
        /// </summary>
        public bool ShortFormAutoGenerate { get; protected set; }

        /// <summary>
        /// Builds a <see cref="Command"/> instance, populated with sub-commands, options, arguments and settings.
        /// </summary>
        /// <returns>A populated <see cref="Command"/> instance.</returns>
        public abstract Command Build();

        /// <summary>
        /// Gets the command builders that are nested/external children of this command builder.
        /// </summary>
        public IEnumerable<CliCommandBuilder> Children => GetChildren(DefinitionType);

        /// <summary>
        /// Registers this command builder so that it can be found by the definition class
        /// and it can be found by the parent definition class if it's a nested/external child.
        /// </summary>
        public void Register()
        {
            if (DefinitionType != null)
                Register(DefinitionType, this);

            if (ParentDefinitionType != null)
                RegisterAsChild(ParentDefinitionType, this);
        }

        #region Static

        private static readonly Dictionary<Type, CliCommandBuilder> RegisteredDefinitionTypes =
            new Dictionary<Type, CliCommandBuilder>();

        private static readonly Dictionary<Type, HashSet<CliCommandBuilder>> RegisteredParentDefinitionTypes =
            new Dictionary<Type, HashSet<CliCommandBuilder>>();

        /// <summary>
        /// Registers a command builder so that it can be found by the definition class.
        /// </summary>
        /// <param name="commandBuilder">A command builder which builds a <see cref="Command"/>.</param>
        /// <typeparam name="TDefinition">The definition class.</typeparam>
        public static void Register<TDefinition>(CliCommandBuilder commandBuilder)
        {
            var definitionType = typeof(TDefinition);

            Register(definitionType, commandBuilder);
        }

        /// <summary>
        /// Registers a command builder so that it can be found by the definition class.
        /// </summary>
        /// <param name="definitionType">The type of the definition class.</param>
        /// <param name="commandBuilder">A command builder which builds a <see cref="Command"/>.</param>
        public static void Register(Type definitionType, CliCommandBuilder commandBuilder)
        {
            RegisteredDefinitionTypes[definitionType] = commandBuilder;
        }

        /// <summary>
        /// Gets the command builder registered for the definition class.
        /// </summary>
        /// <typeparam name="TDefinition">The definition class.</typeparam>
        /// <returns>The registered <see cref="CliCommandBuilder" /> instance.</returns>
        public static CliCommandBuilder Get<TDefinition>()
        {
            var definitionType = typeof(TDefinition);

            return Get(definitionType);
        }

        /// <summary>
        /// Gets the command builder registered for the definition class.
        /// </summary>
        /// <param name="definitionType">The type of the definition class.</param>
        /// <returns>The registered <see cref="CliCommandBuilder" /> instance.</returns>
        public static CliCommandBuilder Get(Type definitionType)
        {
            if (!RegisteredDefinitionTypes.TryGetValue(definitionType, out var commandBuilder))
                throw new Exception($"A registered command builder is not found for '{definitionType}'. " +
                                    $"Please ensure the source generator is running and generating a command builder for your definition class.");

            return commandBuilder;
        }

        /// <summary>
        /// Registers a command builder as a nested/external child so that it can be found by the parent definition class.
        /// </summary>
        /// <param name="childCommandBuilder">The nested/external child command builder.</param>
        /// <typeparam name="TParentDefinition">The parent definition class.</typeparam>
        public static void RegisterAsChild<TParentDefinition>(CliCommandBuilder childCommandBuilder)
        {
            var parentDefinitionType = typeof(TParentDefinition);

            RegisterAsChild(parentDefinitionType, childCommandBuilder);
        }

        /// <summary>
        /// Registers a command builder as a nested/external child so that it can be found by the parent definition class.
        /// </summary>
        /// <param name="parentDefinitionType">The type of the parent definition class.</param>
        /// <param name="childCommandBuilder">The nested/external child command builder.</param>
        public static void RegisterAsChild(Type parentDefinitionType, CliCommandBuilder childCommandBuilder)
        {
            if (!RegisteredParentDefinitionTypes.TryGetValue(parentDefinitionType, out var children))
                RegisteredParentDefinitionTypes[parentDefinitionType] = children = new HashSet<CliCommandBuilder>();

            children.Add(childCommandBuilder);
        }

        /// <summary>
        /// Gets the command builders that are registered as nested/external children of a parent definition.
        /// </summary>
        /// <typeparam name="TParentDefinition">The parent definition class.</typeparam>
        /// <returns>An enumerable whose elements are the <see cref="CliCommandBuilder" /> instances registered as nested/external children.</returns>
        public static IEnumerable<CliCommandBuilder> GetChildren<TParentDefinition>()
        {
            var parentDefinitionType = typeof(TParentDefinition);

            return GetChildren(parentDefinitionType);
        }

        /// <summary>
        /// Gets the command builders that are registered as nested/external children of a parent definition.
        /// </summary>
        /// <param name="parentDefinitionType">The type of the parent definition class.</param>
        /// <returns>An enumerable whose elements are the <see cref="CliCommandBuilder" /> instances registered as nested/external children.</returns>
        public static IEnumerable<CliCommandBuilder> GetChildren(Type parentDefinitionType)
        {
            if (parentDefinitionType == null
                || !RegisteredParentDefinitionTypes.TryGetValue(parentDefinitionType, out var children))
                return Enumerable.Empty<CliCommandBuilder>();

            return children;
        }

        #endregion
    }
}
