-- ��ɫ�� Ŀǰ��2�ֽ�ɫ admin user
CREATE TABLE Roles
(
    ids INT IDENTITY ,
    name NVARCHAR(188) NOT NULL DEFAULT ( '' )
)

-- �Ȳ���2����ɫ
INSERT INTO [dbo].[Roles] ( [name] )
            SELECT  'admin'
            UNION ALL
            SELECT  'user'

-- �û���
CREATE TABLE users
(
    ids INT IDENTITY ,
    --�绰����
    mobile VARCHAR(25) NOT NULL PRIMARY KEY ,
    --ͷ��
    avatar NVARCHAR(88) NOT NULL DEFAULT ( '' ) ,
    -- ����
    password NVARCHAR(188) NOT NULL DEFAULT ( '' ) ,
    -- ����
    name NVARCHAR(188) NOT NULL DEFAULT ( '' ) ,
    -- email
    email NVARCHAR(188) NOT NULL DEFAULT ( '' ) ,
    -- ���һ�ε�¼ʱ��
    lastlogindate DATETIME ,
    -- �ۼƵ�¼����
    logintimes INT NOT NULL DEFAULT ( 0 ) ,

    -- ��¼�� ��� �޸� ɾ��ʱ��
    adddate DATETIME ,
    updatedate DATETIME ,
    deletedate DATETIME ,
)