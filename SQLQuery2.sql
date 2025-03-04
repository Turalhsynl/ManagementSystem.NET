CREATE TABLE AllergenGroup(
	Id INT PRIMARY KEY IDENTITY(1,1),
	[Name] NVARCHAR(50) NOT NULL,
	[Code] NVARCHAR(50) NOT NULL,
	[UpdatedBy] INT NULL,
	[DeletedBy] INT NULL,
	[CreatedBy] INT NULL,
	[CreatedDate] DATETIME2(7) NULL,
	[DeletedDate] DATETIME2(7) NULL,
	[UpdatedDate] DATETIME2(7) NULL,
	[IsDeleted] BIT NOT NULL DEFAULT 0
)

CREATE TABLE Department(
	Id INT PRIMARY KEY IDENTITY(1,1),
	[Name] NVARCHAR(50) NOT NULL,
	[UpdatedBy] INT NULL,
	[DeletedBy] INT NULL,
	[CreatedBy] INT NULL,
	[CreatedDate] DATETIME2(7) NULL,
	[DeletedDate] DATETIME2(7) NULL,
	[UpdatedDate] DATETIME2(7) NULL,
	[IsDeleted] BIT NOT NULL DEFAULT 0
)

CREATE TABLE Ingredient(
	Id INT PRIMARY KEY IDENTITY(1,1),
	[Name] NVARCHAR(100) NOT NULL,
	[Type] NVARCHAR(100) NOT NULL,
	[Barcode] NVARCHAR(100) NOT NULL,
	[ButtonColor] NVARCHAR(100) NOT NULL,
	[TextColor] NVARCHAR(100) NOT NULL,
	[IvoiceNumber] NVARCHAR(100) NOT NULL,
	[Price] MONEY NOT NULL,
	[OpenPrice] BIT NOT NULL,
	[MinimumCount] INT NULL,
	[MaximumCount] INT NULL,
	[FreeIngredientCount] INT NULL,
	[UpdatedBy] INT NULL,
	[DeletedBy] INT NULL,
	[CreatedBy] INT NULL,
	[CreatedDate] DATETIME2(7) NULL,
	[DeletedDate] DATETIME2(7) NULL,
	[UpdatedDate] DATETIME2(7) NULL,
	[IsDeleted] BIT NOT NULL DEFAULT 0
)

CREATE TABLE DepartmentsId(
	Id INT PRIMARY KEY IDENTITY(1,1)
)

CREATE TABLE IngredientDepartments(
	DepartmentId INT,
	IngredientId INT,
	PRIMARY KEY(DepartmentId,IngredientId),
	FOREIGN KEY (DepartmentId) REFERENCES Department(Id),
	FOREIGN KEY (IngredientId) REFERENCES Ingredient(Id)
)

CREATE TABLE Product (
	Id INT PRIMARY KEY IDENTITY(1,1),
	[Name] NVARCHAR(100) NOT NULL,
	[Type] NVARCHAR(100) NOT NULL,
	[Barcode] NVARCHAR(100) NOT NULL,
	[ButtonColor] NVARCHAR(100) NOT NULL,
	[TextColor] NVARCHAR(100) NOT NULL,
	[IvoiceNumber] NVARCHAR(100) NOT NULL,
	[Price] MONEY NOT NULL,
	[OpenPrice] BIT NOT NULL,
	[UpdatedBy] INT NULL,
	[DeletedBy] INT NULL,
	[CreatedBy] INT NULL,
	[CreatedDate] DATETIME2(7) NULL,
	[DeletedDate] DATETIME2(7) NULL,
	[UpdatedDate] DATETIME2(7) NULL,
	[IsDeleted] BIT NOT NULL DEFAULT 0
)


CREATE TABLE ProductIngredient(
	ProductId INT,
	IngredientId INT,
	PRIMARY KEY(ProductId,IngredientId),
	FOREIGN KEY (ProductId) REFERENCES Product(Id),
	FOREIGN KEY (IngredientId) REFERENCES Ingredient(Id)
)

CREATE TABLE ProductDepartment(
	ProductId INT,
	DepartmentId INT,
	PRIMARY KEY(ProductId,DepartmentId),
	FOREIGN KEY (ProductId) REFERENCES Product(Id),
	FOREIGN KEY (DepartmentId) REFERENCES Department(Id)
)

CREATE TABLE ProductAllergenGroup(
	ProductId INT,
	AllergenGroupId INT,
	PRIMARY KEY(ProductId,AllergenGroupId),
	FOREIGN KEY (ProductId) REFERENCES Product(Id),
	FOREIGN KEY (AllergenGroupId) REFERENCES AllergenGroup(Id)	
)