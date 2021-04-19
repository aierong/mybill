-- 角色表 目前就2种角色 admin user
CREATE TABLE Roles
(
    ids INT IDENTITY ,
    name NVARCHAR(188) NOT NULL DEFAULT ( '' )
)

-- 先插入2个角色
INSERT INTO [dbo].[Roles] ( [name] )
            SELECT  'admin'
            UNION ALL
            SELECT  'user'

-- 用户表
CREATE TABLE users
(
    ids INT IDENTITY ,
    --电话号码
    mobile VARCHAR(25) NOT NULL PRIMARY KEY ,
    --头像
    avatar NVARCHAR(88) NOT NULL DEFAULT ( '' ) ,
    -- 密码
    password NVARCHAR(188) NOT NULL DEFAULT ( '' ) ,
    -- 姓名
    name NVARCHAR(188) NOT NULL DEFAULT ( '' ) ,
    -- email
    email NVARCHAR(188) NOT NULL DEFAULT ( '' ) ,
    -- 最后一次登录时间
    lastlogindate DATETIME ,
    -- 累计登录次数
    logintimes INT NOT NULL DEFAULT ( 0 ) ,

    -- 记录的 添加 修改 删除时间
    adddate DATETIME ,
    updatedate DATETIME ,
    deletedate DATETIME ,
)