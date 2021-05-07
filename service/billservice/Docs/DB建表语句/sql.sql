-- ��ɫ�� Ŀǰ��2�ֽ�ɫ admin user
CREATE TABLE roles
(
    ids INT IDENTITY ,
    -- ��ɫ��
    name NVARCHAR(28) NOT NULL DEFAULT ( '' ) PRIMARY KEY
)

-- �Ȳ���2����ɫ
INSERT INTO [dbo].[roles] ( [name] )
            SELECT  'admin'
            UNION ALL
            SELECT  'user'
go



-- �û���
CREATE TABLE users
(
    ids INT IDENTITY ,
    -- ��ɫ�� 
    rolename NVARCHAR(28) NOT NULL DEFAULT ( 'user' ) ,
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
    adddate DATETIME NOT NULL DEFAULT ( GETDATE ()) ,
    updatedate DATETIME ,
    deletedate DATETIME ,

    --ɾ����־,��ʱ����
    delmark VARCHAR(1) NOT NULL DEFAULT ( 'N' )
)

-- 202cb962ac59075b964b07152d234b70             123ǰ���������
-- 2bHX20zW5wk1Nooe+xDjdw==                     123���ݿ�������

--��Ĭ�ϲ��� һ������Ա
INSERT INTO [dbo].[users] (
                              [rolename] ,
                              [mobile] ,
                              [avatar] ,
                              [password] ,
                              [name] ,
                              [email]
                          )
            SELECT  'admin' ,
                    '13912345678' ,
                    'smile-o' ,
                    '2bHX20zW5wk1Nooe+xDjdw==' ,
                    '����Ա' ,
                    'aierong@126.com'

CREATE TABLE billtype
(
    ids INT IDENTITY PRIMARY KEY ,
    -- ��֧������
    isout BIT NOT NULL DEFAULT ( 0 ) ,
    -- ��ϵͳԤ��������
    issystemtype BIT NOT NULL DEFAULT ( 1 ) ,
    -- ��������
    typename NVARCHAR(38) NOT NULL DEFAULT ( '' ) ,
	-- ͼ��
	avatar NVARCHAR(88) NOT NULL DEFAULT ( '' ) ,

    -- �ĸ��û�������
    mobile VARCHAR(25) NOT NULL DEFAULT ( '' ) ,

    -- ��¼�� ��� �޸� ɾ��ʱ��
    adddate DATETIME NOT NULL DEFAULT ( GETDATE ()) ,
    updatedate DATETIME ,
    deletedate DATETIME
)

-- һЩϵͳ���õ�����
INSERT INTO [dbo].[billtype]
           ([isout]
           ,[issystemtype]
           ,[typename])
SELECT 1,1,'����'  
UNION ALL
SELECT 1,1,'����'  
UNION ALL
SELECT 1,1,'ס��'  
UNION ALL
SELECT 1,1,'����'  
UNION ALL
SELECT 0,1,'����'  
UNION ALL
SELECT 0,1,'����'  
UNION ALL
SELECT 0,1,'���'  
UNION ALL
SELECT 0,1,'ת��'  



-- ��Ŀ
CREATE TABLE bills
(
    ids INT IDENTITY PRIMARY KEY ,
    -- �ĸ��û�
    mobile VARCHAR(25) NOT NULL DEFAULT ( '' ) ,
    -- ����id
    billtypeid INT NOT NULL DEFAULT ( 0 ) ,
	-- ��֧������
    isout BIT NOT NULL DEFAULT ( 0 ) ,
    -- ��� 
    moneys MONEY NOT NULL DEFAULT ( 0 ) ,
    -- �������� 2020-01-01
    moneydate NVARCHAR(168) NOT NULL DEFAULT ( '' ) ,
	
	moneyyear INT NOT NULL DEFAULT(0),
	moneymonth INT NOT NULL DEFAULT(0) ,
	moneyday INT NOT NULL DEFAULT(0),

    --��ע
    memo NVARCHAR(168) DEFAULT ( '' ) ,

	-- ��Դ,��ʱ����
	sources  NVARCHAR(168) DEFAULT ( '' ) ,

    -- ��¼�� ��� �޸� ɾ��ʱ��
    adddate DATETIME NOT NULL DEFAULT ( GETDATE ()) ,
    updatedate DATETIME ,
    deletedate DATETIME ,

    --ɾ����־
    delmark VARCHAR(1) NOT NULL DEFAULT ( 'N' )
)

-- ��һ������
CREATE INDEX idx_bills
    ON bills ( mobile )
