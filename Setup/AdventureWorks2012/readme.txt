1. Copy File to:
    
   C:\Program Files\Microsoft SQL Server\MSSQL11.SQLSERVER2012\MSSQL\DATA

2. Execute SQL-Statement:

   CREATE DATABASE AdventureWorks2012
   ON (FILENAME = 'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLSERVER2012\MSSQL\DATA\AdventureWorks2012_CS_Data.mdf')
   FOR ATTACH_REBUILD_LOG ; 

3. Execute SQL-Commands:
  
--Generation Script 
--SELECT 'ALTER TABLE ['+SCHEMA_NAME(schema_id)+'].['+name+'] ADD [RowVersion] [timestamp] NOT NULL;'
--AS SchemaTable
--FROM sys.tables 
   
ALTER TABLE [Production].[ScrapReason] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [HumanResources].[Shift] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Production].[ProductCategory] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Purchasing].[ShipMethod] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Production].[ProductCostHistory] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Production].[ProductDescription] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Sales].[ShoppingCartItem] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Production].[ProductDocument] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [dbo].[DatabaseLog] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Production].[ProductInventory] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Sales].[SpecialOffer] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [dbo].[ErrorLog] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Production].[ProductListPriceHistory] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Person].[Address] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Sales].[SpecialOfferProduct] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Production].[ProductModel] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Person].[AddressType] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Person].[StateProvince] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Production].[ProductModelIllustration] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [dbo].[AWBuildVersion] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Production].[ProductModelProductDescriptionCulture] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Production].[BillOfMaterials] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Sales].[Store] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Production].[ProductPhoto] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Production].[ProductProductPhoto] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Production].[TransactionHistory] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Production].[ProductReview] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Person].[BusinessEntity] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Production].[TransactionHistoryArchive] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Production].[ProductSubcategory] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Person].[BusinessEntityAddress] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Purchasing].[ProductVendor] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Person].[BusinessEntityContact] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Production].[UnitMeasure] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Purchasing].[Vendor] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Person].[ContactType] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Sales].[CountryRegionCurrency] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Person].[CountryRegion] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Production].[WorkOrder] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Purchasing].[PurchaseOrderDetail] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Sales].[CreditCard] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Production].[Culture] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Production].[WorkOrderRouting] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Sales].[Currency] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Purchasing].[PurchaseOrderHeader] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Sales].[CurrencyRate] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Sales].[Customer] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [HumanResources].[Department] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Production].[Document] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Sales].[SalesOrderDetail] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Person].[EmailAddress] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [HumanResources].[Employee] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Sales].[SalesOrderHeader] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [HumanResources].[EmployeeDepartmentHistory] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [HumanResources].[EmployeePayHistory] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Sales].[SalesOrderHeaderSalesReason] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Sales].[SalesPerson] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Production].[Illustration] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [HumanResources].[JobCandidate] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Production].[Location] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Person].[Password] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Sales].[SalesPersonQuotaHistory] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Person].[Person] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Sales].[SalesReason] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Sales].[SalesTaxRate] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Sales].[PersonCreditCard] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Person].[PersonPhone] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Sales].[SalesTerritory] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Person].[PhoneNumberType] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Production].[Product] ADD [RowVersion] [timestamp] NOT NULL;
ALTER TABLE [Sales].[SalesTerritoryHistory] ADD [RowVersion] [timestamp] NOT NULL;


4. Temporary Updates:

DROP Table Production.ProductDocument
DROP Table Production.Document
