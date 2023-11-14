CREATE
LOGIN samplelogin WITH PASSWORD = 'sample$P@ssword'

create
database SampleDb collate SQL_Latin1_General_CP1_CI_AS
go
       
use SampleDb
go

create table dbo.Users
(
    Id          int identity
        constraint Users_pk
        primary key,
    Name        nvarchar(255) not null,
    Email       nvarchar(255) not null,
    DateAdded   datetime not null,
    LastUpdated datetime
)
    go

create index Users_Email_index
    on dbo.Users (Email) go
    
use SampleDb
go

create table dbo.Follows
(
    Id            int identity
        constraint Follows_pk
        primary key,
    UserId        int      not null,
    FollowsUserId int      not null,
    DateFollowed  datetime not null
)
    go

create index Follows_UserId_FollowsUserId_index
    on dbo.Follows (UserId, FollowsUserId)
    go

CREATE
USER samplelogin FOR LOGIN samplelogin
GO
       
GRANT
SELECT, EXECUTE
ON schema::dbo TO samplelogin
    GO