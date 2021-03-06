/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 [Id]
      ,[Name]
      ,[GrowthPoint]
      ,[FreeFreightPoint]
      ,[CommentGrowthPoint]
      ,[PriviledgeFreeFreight]
      ,[PriviledgeSignIn]
      ,[PrivilegeComment]
      ,[PrivilegePromotin]
      ,[PrivilegeMemberPrice]
      ,[PrivilegeBirthDay]
      ,[Note]
      ,[TenantId]
  FROM [Tiger].[dbo].[AppMemberLevel]

  SELECT * FROM dbo.StockCheckDetail

  SELECT * FROM dbo.AppCategorys WHERE Id = '621BD6D7-2998-0CC0-7972-39FEA13173CD'

  UPDATE dbo.AppCategorys SET ParentId = NULL

  SELECT * FROM dbo.AppWarehouses

  SELECT t1.Name,t2.Name FROM dbo.AppSupplies t1
  JOIN dbo.AppWarehouses t2 ON t1.WarehouseId = t2.Id

  SELECT * FROM dbo.AppStores

  SELECT * FROM dbo.AppProducts WHERE IsDeleted = 0

  SELECT * FROM dbo.AppComments

  SELECT * FROM dbo.AppProductAttributeTypes

  SELECT * FROM dbo.AppProductAttributes

  SELECT * FROM dbo.AppProductAttributeValues

  SELECT *FROM dbo.AppOrders

  UPDATE dbo.AppOrders SET PaymentTime = NULL

  SELECT * FROM dbo.AppMembers

  SELECT * FROM dbo.AppMemberLevel

  SELECT * FROM dbo.AppCoupons

  SELECT * FROM dbo.AppCouponHistories

  SELECT * FROM dbo.AppCartItems

  DELETE FROM dbo.AppCartItems

  SELECT * FROM dbo.AppOrders
  SELECT * FROM dbo.AppProducts
  SELECT * FROM dbo.AppOrderItems

  SELECT * FROM dbo.AppMembers

  SELECT * FROM dbo.AppSkus

SELECT *FROM dbo.AppMemberLevel

SELECT * FROM dbo.StockReceiptHeader WHERE id = '164346b3-133a-49df-ac7f-39fecd04b8b6' AND IsDeleted = 1

SELECT * FROM dbo.StockReceiptDetail WHERE ReceiptHeaderId = '164346b3-133a-49df-ac7f-39fecd04b8b6' AND IsDeleted = 1

DELETE FROM StockReceiptDetail

DELETE FROM StockReceiptHeader

SELECT * FROM dbo.AppProducts WHERE Id = '9CAC5265-21DC-C016-0374-39FEB4686D17'
SELECT * FROM dbo.AppMembers


SELECT *FROM dbo.StockShipmentDetail WHERE ShipmentId = '8F847087-B4A9-2ADE-31F9-39FED28525B4'

SELECT *FROM dbo.StockShipmentHeader

SELECT *FROM dbo.appWarehouses

SELECT *FROM dbo.StockTransferDetail

SELECT * FROM dbo.StockTransferHeader

--SELECT * FROM 
SELECT * FROM AppCategorys

UPDATE dbo.AppCategorys SET Name = '中国好搜阿富汗冬奥会覅大搜房活动按时发货的司法和屌丝哈佛我爱迪生覅和覅和佛山发货' WHERE id = '53AC8E93-322C-B01D-EFC5-39FEB4681576'


--SELECT *FROM AppOrderReturnHeaders

SELECT * FROM AppPurchaseReturnDetail

SELECT * FROM dbo.AppMembers

SELECT *FROM dbo.AppPurchaseHeader
SELECT * FROM dbo.AppPurchaseDetail WHERE PurchaseHeaderId = 'd9965d37-998b-d47c-fe01-39feda36bddd'

UPDATE dbo.AppPurchaseDetail SET PurchaseHeaderId = 'D9965D37-998B-D47C-FE01-39FEDA36BDDD'

SELECT * FROM dbo.AbpUsers WHERE Id = 'F4F78ACC-4B5E-3CA9-BA3F-39FE8A3170E4'