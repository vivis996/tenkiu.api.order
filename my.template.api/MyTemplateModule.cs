using Autofac;

namespace my.template.api;

/// <summary>
/// Defines a module for Autofac that registers types from the current assembly.
/// Types are registered as their implemented interfaces and also as themselves.
/// </summary>
public class MyTemplateModule : Module
{
  /// <summary>
  /// Loads the registrations for the current assembly into the Autofac container.
  /// </summary>
  /// <param name="builder">The container builder used to register services.</param>
  protected sealed override void Load(ContainerBuilder builder)
  {
    builder.RegisterTypes(this.ThisAssembly.GetTypes())
           .AsImplementedInterfaces()
           .AsSelf();
  }
}
