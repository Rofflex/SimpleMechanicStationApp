IF OBJECT_ID (N'dbo.[LaborOrder]', N'U') IS NOT NULL		--14
DROP TABLE dbo.[LaborOrder];
GO

IF OBJECT_ID (N'dbo.[Mechanic]', N'U') IS NOT NULL			--13
DROP TABLE dbo.[Mechanic];
GO

IF OBJECT_ID (N'dbo.[PartOrder]', N'U') IS NOT NULL			--12
DROP TABLE dbo.[PartOrder];
GO

IF OBJECT_ID (N'dbo.[Order]', N'U') IS NOT NULL				--11
DROP TABLE dbo.[Order];
GO

IF OBJECT_ID (N'dbo.[CarVIN]', N'U') IS NOT NULL			--10
DROP TABLE dbo.[CarVIN];
GO

IF OBJECT_ID (N'dbo.[VINList]', N'U') IS NOT NULL			--9
DROP TABLE dbo.[VINList];
GO

IF OBJECT_ID (N'dbo.[Labor]', N'U') IS NOT NULL				--8
DROP TABLE dbo.[Labor];
GO

IF OBJECT_ID (N'dbo.[CarPart_CarInfo]', N'U') IS NOT NULL	--7
DROP TABLE dbo.[CarPart_CarInfo];
GO

IF OBJECT_ID (N'dbo.[CarPart]', N'U') IS NOT NULL			--6
DROP TABLE dbo.[CarPart];
GO

IF OBJECT_ID (N'dbo.[ManufactureWarehouse]', N'U') IS NOT NULL--5
DROP TABLE dbo.[ManufactureWarehouse];
GO

IF OBJECT_ID (N'dbo.[Manufacture]', N'U') IS NOT NULL		--4
DROP TABLE dbo.[Manufacture];
GO

IF OBJECT_ID (N'dbo.[CarInfo]', N'U') IS NOT NULL			--3 
DROP TABLE dbo.[CarInfo];
GO

IF OBJECT_ID (N'dbo.[Warehouse]', N'U') IS NOT NULL			--2 
DROP TABLE dbo.[Warehouse];
GO

IF OBJECT_ID (N'dbo.[Customer]', N'U') IS NOT NULL			--1
DROP TABLE dbo.[Customer];
GO


create table dbo.[Customer]--1
(
	CustomerId int primary key not null,
	CustomerName varchar(50) null,
	CustomerPhone varchar(12) null,
	CustomerAddress varchar(100) null,
	CustomerInformation varchar(300) null
)

create table dbo.[CarInfo]--2
(
	CarId int primary key not null,
	CarMake varchar(50) null,
	CarModel varchar(50) null,
	CarYear smallint null,
	CarEngine varchar(100) null,
	CarBodyStyle varchar(100) null,
	CarTrimLevel varchar(100) null,
	CarWheelDrive varchar(3) null,
	CarTransmission varchar(50) null,
	Color varchar(50) null,
	CarAdditional varchar(300) null,
	CarLink varchar(300) null
)

create table dbo.[Warehouse]--3
(
	WarehouseId int primary key not null,
	WarehouseName varchar(100) null,
	WarehouseAddress varchar(100) null,
	WarehousePhone varchar(12) null
)

create table dbo.[Manufacture]--4
(
	ManufactureId int primary key not null,
	ManufactureName varchar(100) null,
	ManufacturePhone varchar(12) null
)

create table dbo.[ManufactureWarehouse]--5
(
	WarehouseId int references dbo.[Warehouse](WarehouseId) on update cascade not null,
	ManufactureId int references dbo.[Manufacture](ManufactureId) on update cascade not null,
	constraint WarehouseId_ManufactureId_Unique unique(WarehouseId, ManufactureId)
)


create table dbo.[CarPart]--6
(
	PartId varchar(50),
	PartName varchar(50) null,
	PartDescription varchar(300) null,
	PartLink varchar(300) null,
	PartImage image null,
	PartRetailPrice decimal(8,2) null,
	PartTradePrice decimal(8,2) null,
	ManufactureId int not null references dbo.[Manufacture](ManufactureId) on update cascade,
	constraint PK_PartId_ManufactureId primary key (PartId, ManufactureId)
)

create table dbo.[CarPart_CarInfo]--7
(
	PartId varchar(50) not null,
	ManufactureId int null,
	CarId int not null references dbo.[CarInfo](CarId) on update cascade,
	constraint FK_CarPart_CarInfo_PartId_ManufactureId foreign key (PartId, ManufactureId) references dbo.[CarPart](PartId, ManufactureId) on update cascade
)

create table dbo.[Labor]--8
(
	LaborId int not null primary key,
	LaborName varchar(50) null,
	LaborDescription varchar(300) null,
	LaborLink varchar(300) null,
	LaborHours decimal(5,2) null,
	PartId varchar(50) null,
	ManufactureId int null,
	constraint FK_Labor_PartId_ManufactureId foreign key (PartId, ManufactureId) references dbo.[CarPart](PartId, ManufactureId) on update cascade
)

create table dbo.[VINList]--9
(
	VIN varchar(17) not null primary key
)

create table dbo.[CarVIN]--10
(
	VIN varchar(17) references dbo.[VINList](VIN) on update cascade null,
	CarId int references dbo.[CarInfo](CarId) on update cascade null,
	constraint VIN_CarId_Unique unique(VIN)
)

create table dbo.[Order]--11
(
	OrderId int primary key not null,
	VIN varchar(17) references dbo.[VINList](VIN) on update cascade null,
	CarId int  references dbo.[CarInfo](CarId) on update cascade null, 
	CarPlate varchar(8) null,
	CustomerId int references dbo.[Customer](CustomerId) on update cascade null,
	PartsAmount decimal(10,2) null default 0,
	LaborsAmount decimal(10,2) null default 0,
	OrderAmount decimal(10,2) null default 0,
	OrderOpenDate date null,
	OrderCloseDate date null,
	CarOdometerStart float null,
	CarOdometerFinish float null,
	Discount decimal(5,2) null default 0,
	Deposit decimal(10,2) null default 0,
	SalesTax decimal(5,2) null default 7.25,
	WasteMaterialFee decimal(5,2) null default 5.72,
	WasteMaterialFeeIncluded bit null default 0,
	constraint Discount_Check check(Discount>=0 or Discount<=100)
)

create table dbo.[PartOrder]--12
(
	OrderId int not null references dbo.[Order](OrderId),
	PartId varchar(50) not null, 
	ManufactureId int not null,
	PartSoldPrice decimal(8,2) null,
	Quantity decimal(5,2) null,
	constraint PartId_OrderId_ManufactureId_Unique unique (OrderId, PartId, ManufactureId),
	constraint FK_PartOrder_PartId_ManufactureId foreign key (PartId, ManufactureId) references dbo.[CarPart](PartId, ManufactureId) on update cascade
)

create table dbo.[Mechanic]--13
(
	MechanicId int primary key,
	MechanicName varchar(100)
)

create table dbo.[LaborOrder]--14
(
	OrderId int references dbo.[Order](OrderId) not null,
	LaborId int references dbo.[Labor](LaborId) not null,
	MechanicId int references dbo.[Mechanic](MechanicId) not null,
	LaborSoldPrice decimal(10,2) null,
	LaborSoldHours decimal(10,2) null,
	constraint LaborId_OrderId_MechanicId_Unique unique (OrderId, LaborId, MechanicId)
)

