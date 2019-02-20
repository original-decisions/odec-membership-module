
begin tran
if not exists( select top 1 1 from dbo.AspNetRoles where Name like 'CUSTOMER')
begin
INSERT INTO dbo.AspNetRoles (Name,Scope,InRoleId)
values ('Customer','WebSite',null)
end
if not exists( select top 1 1 from dbo.AspNetRoles where Name like 'Admin')
begin
INSERT INTO dbo.AspNetRoles (Name,Scope,InRoleId)
values ('Admin','WebSite',null)
end
if not exists( select top 1 1 from dbo.AspNetRoles where Name like 'Crafter')
begin
INSERT INTO dbo.AspNetRoles (Name,Scope,InRoleId)
values ('Crafter','WebSite',null)
end
if not exists( select top 1 1 from dbo.AspNetRoles where Name like 'demo')
begin
INSERT INTO dbo.AspNetRoles (Name,Scope,InRoleId)
values ('Demo','WebSite',null)
end
commit tran

