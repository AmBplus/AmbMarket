﻿using _01_Framework.Domain.BaseEntities;
namespace ambMarket.Domain.userAggregate;

public class User : BaseEntityWithIdentity
{
    #region Properties
    public string? Name { get; set; }
    public string? LastName { get;  set; }
  
    #endregion /Properties

    #region Methods
    //public void SetName(string name)
    //{
    //    Name = name;
    //}
    //public void SetLastName(string name)
    //{
    //    Name = name;
    //}

    //public void SetFullName(string name, string lastName)
    //{
    //    Name = name;
    //    LastName = lastName;
    //}
    #endregion /Methods

}