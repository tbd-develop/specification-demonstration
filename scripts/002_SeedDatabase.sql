use SampleDb
go

insert into Users ( [Name], [Email], [DateAdded])
    values
    ( 'John Smith', 'jsmith@demo.com', '2023-01-01'),
    ( 'Jane Doe', 'jdoe@demo.com', '2022-10-15'),
    ( 'Bob Jones', 'bjones@demo.com', '2022-12-19')

insert into Follows ( [UserId], [FollowsUserId], [DateFollowed])
    values
    ( 1, 2, '2023-03-10'),
    ( 2, 1, '2023-04-17'),
    ( 2, 3, '2022-12-21'),
    ( 3, 2, '2023-01-04')