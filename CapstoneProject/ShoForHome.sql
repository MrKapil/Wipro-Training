CREATE DATABASE ShopForHomeDb;

go

USE ShopForHomeDb

go

-- USERS

CREATE TABLE Users (
    UserId BIGINT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(200) NOT NULL,
    Email NVARCHAR(256) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(500) NOT NULL,
    Role NVARCHAR(50) NOT NULL CHECK (Role IN ('User','Admin')),
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSUTCDATETIME()
);

-- CATEGORIES
----------------------------------------------------
CREATE TABLE Categories (
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(150) NOT NULL,
    Slug NVARCHAR(150) NOT NULL UNIQUE,
    IsActive BIT NOT NULL DEFAULT 1
);


-- PRODUCTS
----------------------------------------------------
CREATE TABLE Products (
    ProductId BIGINT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(300) NOT NULL,
    SKU NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(MAX) NULL,
    Price DECIMAL(18,2) NOT NULL,
    Rating DECIMAL(3,2) NULL CHECK (Rating BETWEEN 0 AND 5),
    CategoryId INT NOT NULL,
    ImageFileName NVARCHAR(300) NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_Products_Categories FOREIGN KEY (CategoryId) REFERENCES dbo.Categories(CategoryId)
);


-- INVENTORY
----------------------------------------------------
CREATE TABLE Inventory (
    ProductId BIGINT PRIMARY KEY,
    StockQty INT NOT NULL DEFAULT 0 CHECK (StockQty >= 0),
    CONSTRAINT FK_Inventory_Products FOREIGN KEY (ProductId) REFERENCES dbo.Products(ProductId)
);


-- CARTS
----------------------------------------------------
CREATE TABLE Carts (
    CartId BIGINT IDENTITY(1,1) PRIMARY KEY,
    UserId BIGINT NOT NULL,
    CreatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSUTCDATETIME(),
    UpdatedAt DATETIMEOFFSET NULL,
    CONSTRAINT FK_Carts_Users FOREIGN KEY (UserId) REFERENCES dbo.Users(UserId)
);


-- CART ITEMS
----------------------------------------------------
CREATE TABLE CartItems (
    CartItemId BIGINT IDENTITY(1,1) PRIMARY KEY,
    CartId BIGINT NOT NULL,
    ProductId BIGINT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    CONSTRAINT FK_CartItems_Carts FOREIGN KEY (CartId) REFERENCES dbo.Carts(CartId) ON DELETE CASCADE,
    CONSTRAINT FK_CartItems_Products FOREIGN KEY (ProductId) REFERENCES dbo.Products(ProductId)
);


-- WISHLISTS
----------------------------------------------------
CREATE TABLE Wishlists (
    WishlistId BIGINT IDENTITY(1,1) PRIMARY KEY,
    UserId BIGINT NOT NULL,
    CreatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_Wishlists_Users FOREIGN KEY (UserId) REFERENCES dbo.Users(UserId)
);


-- WISHLIST ITEMS
----------------------------------------------------
CREATE TABLE WishlistItems (
    WishlistItemId BIGINT IDENTITY(1,1) PRIMARY KEY,
    WishlistId BIGINT NOT NULL,
    ProductId BIGINT NOT NULL,
    CONSTRAINT FK_WishlistItems_Wishlist FOREIGN KEY (WishlistId) REFERENCES dbo.Wishlists(WishlistId) ON DELETE CASCADE,
    CONSTRAINT FK_WishlistItems_Products FOREIGN KEY (ProductId) REFERENCES dbo.Products(ProductId)
);


-- ORDERS
----------------------------------------------------
CREATE TABLE Orders (
    OrderId BIGINT IDENTITY(1,1) PRIMARY KEY,
    UserId BIGINT NOT NULL,
    TotalAmount DECIMAL(18,2) NOT NULL,
    DiscountAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    FinalAmount DECIMAL(18,2) NOT NULL,
    Status NVARCHAR(50) NOT NULL DEFAULT 'Created',
    CreatedAt DATETIMEOFFSET NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT FK_Orders_Users FOREIGN KEY (UserId) REFERENCES dbo.Users(UserId)
);


-- ORDER ITEMS
----------------------------------------------------
CREATE TABLE OrderItems (
    OrderItemId BIGINT IDENTITY(1,1) PRIMARY KEY,
    OrderId BIGINT NOT NULL,
    ProductId BIGINT NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    LineTotal AS (UnitPrice * Quantity) PERSISTED, -- computed column
    CONSTRAINT FK_OrderItems_Orders FOREIGN KEY (OrderId) REFERENCES dbo.Orders(OrderId) ON DELETE CASCADE,
    CONSTRAINT FK_OrderItems_Products FOREIGN KEY (ProductId) REFERENCES dbo.Products(ProductId)
);


-- COUPONS
----------------------------------------------------
CREATE TABLE Coupons (
    CouponId BIGINT IDENTITY(1,1) PRIMARY KEY,
    Code NVARCHAR(100) NOT NULL UNIQUE,
    Type NVARCHAR(20) NOT NULL CHECK (Type IN ('Percent','Fixed')),
    Value DECIMAL(18,2) NOT NULL CHECK (Value > 0),
    StartDate DATETIMEOFFSET NULL,
    EndDate DATETIMEOFFSET NULL,
    MinOrderAmount DECIMAL(18,2) NULL,
    MaxDiscount DECIMAL(18,2) NULL,
    UsageLimitPerUser INT NULL,
    IsActive BIT NOT NULL DEFAULT 1
);


-- COUPON ASSIGNMENTS
----------------------------------------------------
CREATE TABLE CouponAssignments (
    CouponAssignmentId BIGINT IDENTITY(1,1) PRIMARY KEY,
    CouponId BIGINT NOT NULL,
    UserId BIGINT NOT NULL,
    CONSTRAINT FK_Assign_Coupon FOREIGN KEY (CouponId) REFERENCES dbo.Coupons(CouponId),
    CONSTRAINT FK_Assign_User FOREIGN KEY (UserId) REFERENCES dbo.Users(UserId)
);


-- APPLIED COUPONS
----------------------------------------------------
CREATE TABLE AppliedCoupons (
    AppliedCouponId BIGINT IDENTITY(1,1) PRIMARY KEY,
    OrderId BIGINT NOT NULL,
    CouponId BIGINT NOT NULL,
    CONSTRAINT FK_Applied_Order FOREIGN KEY (OrderId) REFERENCES dbo.Orders(OrderId),
    CONSTRAINT FK_Applied_Coupon FOREIGN KEY (CouponId) REFERENCES dbo.Coupons(CouponId)
);


-- AUDIT LOGS (optional-useful)
----------------------------------------------------
CREATE TABLE AuditLogs (
    AuditId BIGINT IDENTITY(1,1) PRIMARY KEY,
    UserId BIGINT NULL,
    Action NVARCHAR(50) NOT NULL,
    Entity NVARCHAR(100) NOT NULL,
    EntityId BIGINT NULL,
    Timestamp DATETIMEOFFSET NOT NULL DEFAULT SYSUTCDATETIME(),
    Details NVARCHAR(MAX) NULL,
    CONSTRAINT FK_Audit_Users FOREIGN KEY (UserId) REFERENCES dbo.Users(UserId)
);


----------------------------------------------------
-- INDEXES
----------------------------------------------------
CREATE INDEX IX_Products_CategoryId ON dbo.Products(CategoryId);
CREATE INDEX IX_CartItems_CartId ON dbo.CartItems(CartId);
CREATE INDEX IX_OrderItems_OrderId ON dbo.OrderItems(OrderId);
CREATE INDEX IX_Inventory_StockQty ON dbo.Inventory(StockQty);
CREATE INDEX IX_Orders_UserId_CreatedAt ON dbo.Orders(UserId, CreatedAt);