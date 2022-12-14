using System.Windows;
using System.Windows.Markup;

namespace Easy.Toolkit
{
    /// <summary>
    /// Provides Types and Services registered with the Container
    /// <example>
    /// <para>
    /// Usage as markup extension:
    /// <![CDATA[
    ///   <TextBlock
    ///     Text="{Binding
    ///       Path=Foo,
    ///       Converter={easy:Container {x:Type local:MyConverter}}}" />
    /// ]]>
    /// </para>
    /// <para>
    /// Usage as XML element:
    /// <![CDATA[
    ///   <Window>
    ///     <Window.DataContext>
    ///       <easy:Container ServiceType="{x:Type local:MyViewModel}" />
    ///     </Window.DataContext>
    ///   </Window>
    /// ]]>
    /// </para>
    /// </example>
    /// </summary>
    [MarkupExtensionReturnType(typeof(object))]
    [Localizability(LocalizationCategory.NeverLocalize)]
    public class ContainerExtension : MarkupExtension
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerExtension"/> class.
        /// </summary>
        public ContainerExtension()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerExtension"/> class.
        /// </summary>
        /// <param name="serviceType">The type to Resolve</param>
        public ContainerExtension(Type serviceType)
        {
            ServiceType = serviceType;
        }

        /// <summary>
        /// The type to Resolve
        /// </summary>
        public Type ServiceType { get; set; }

        /// <summary>
        /// Provide resolved object from <see cref="ContainerLocator.Container"/>
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (ServiceType == null)
            {
                return default;
            }

            object serviceTypeInstance = ContainerLocator.Container.Resolve(ServiceType);

            return serviceTypeInstance;
        }
    }
}
