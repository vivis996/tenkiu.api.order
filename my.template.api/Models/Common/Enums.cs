namespace my.template.api.Models.Common;

/// <summary>
/// Defines the types of users and their roles within the system.
/// </summary>
public enum UserType
{
  /// <summary>
  /// Represents no assigned user type.
  /// </summary>
  None = 0,

  /// <summary>
  /// Represents internal system processes or accounts.
  /// </summary>
  System = 1,

  /// <summary>
  /// Represents a super user with elevated privileges.
  /// </summary>
  SuperUser = 2,

  /// <summary>
  /// Represents an administrator with management privileges.
  /// </summary>
  Admin = 3,

  /// <summary>
  /// Represents a regular user with standard access.
  /// </summary>
  Normal = 4,

  /// <summary>
  /// Represents a guest user with limited access.
  /// </summary>
  Guest = 5,

  /// <summary>
  /// Represents a custom-defined user type.
  /// </summary>
  Custom = 6,
}
