using System.Data;
using Microsoft.EntityFrameworkCore;

namespace my.template.api.Context;

public class MyDbContext : DbContext, IDbContext
{
  private readonly IDbConnection _dbConnection;

  public MyDbContext(IDbConnection dbConnection)
  {
    this._dbConnection = dbConnection;
  }

  public MyDbContext(DbContextOptions<MyDbContext> options, IDbConnection dbConnection)
    : base(options)
  {
    this._dbConnection = dbConnection;
  }

  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    optionsBuilder.UseMySql(_dbConnection.ConnectionString, ServerVersion.AutoDetect(_dbConnection.ConnectionString));
  }
}
