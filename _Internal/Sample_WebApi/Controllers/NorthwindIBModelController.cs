﻿#define DBCONTEXT_PROVIDER 

using System;
using System.Linq;
using System.Web.Http;
using Breeze.WebApi;
using Newtonsoft.Json.Linq;
using Sample_WebApi.Models;
using System.Collections.Generic;


namespace Sample_WebApi.Controllers {

  public class NorthwindContextProvider: EFContextProvider<NorthwindIBContext>  {
    public NorthwindContextProvider(String contextName)
      : base(contextName) {
    }

    public override bool BeforeSaveEntity(EntityInfo entityInfo) {
      return true;
    }

    public override Dictionary<Type, List<EntityInfo>> BeforeSaveEntities(Dictionary<Type, List<EntityInfo>> saveMap) {
      return saveMap;
    }
      
    //public bool SaveCustomer(Customer c, EntityInfo entityInfo) {
      
    //}

  }

  public class NorthwindIBModelController : ApiController {

#if DBCONTEXT_PROVIDER 
    EFContextProvider<NorthwindIBContext> ContextProvider = new NorthwindContextProvider("NorthwindIBContext");
#else
    EFContextProvider<NorthwindIBEntities> ContextProvider = new NorthwindContextProvider("NorthwindIBEntities") ;
#endif

    [AcceptVerbs("GET")]
    public String Metadata() {
      return ContextProvider.Metadata();
    }

    [AcceptVerbs("POST")]
    public SaveResult SaveChanges(JObject saveBundle) {
      return ContextProvider.SaveChanges(saveBundle);
    }

    [AcceptVerbs("GET")]
    public IQueryable<Customer> Customers() {
      var custs = ContextProvider.Context.Customers;
      return custs;
    }

    [AcceptVerbs("GET")]
    public IQueryable<Customer> CustomersAndOrders() {
      var custs = ContextProvider.Context.Customers.Include("Orders");
      return custs;
    }

    [AcceptVerbs("GET")]
    public IQueryable<Customer> CustomersStartingWithA() {
      var custs = ContextProvider.Context.Customers.Where(c => c.CompanyName.StartsWith("A"));
      return custs;
    }


    [AcceptVerbs("GET")]
    public IQueryable<Order> Orders() {
      var orders = ContextProvider.Context.Orders;
      return orders;
    }

    [AcceptVerbs("GET")]
    public IQueryable<Order> OrdersAndCustomers() {
      var orders = ContextProvider.Context.Orders.Include("Customer");
      return orders;
    }

    // [Queryable]
    [AcceptVerbs("GET")]
    public IQueryable<Employee> Employees() {
      return ContextProvider.Context.Employees;
    }

    // [Queryable]
    [AcceptVerbs("GET")]
    public IQueryable<OrderDetail> OrderDetails() {
      return ContextProvider.Context.OrderDetails;
    }

    // [Queryable]
    [AcceptVerbs("GET")]
    public IQueryable<Product> Products() {
      return ContextProvider.Context.Products;
    }

    // [Queryable]
    [AcceptVerbs("GET")]
    public IQueryable<Region> Regions() {
      return ContextProvider.Context.Regions;
    }

    //[Queryable]
    [AcceptVerbs("GET")]
    public IQueryable<Territory> Territories() {
      return ContextProvider.Context.Territories;
    }
    
  }

  

}