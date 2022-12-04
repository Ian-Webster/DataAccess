--liquibase formatted sql

--changeset webster:1.2
insert into dbo."Book" ("BookId", "Name") values ('2c4a20f5-3fc8-4694-9c03-cb444bf416dc', 'Fight Club');
insert into dbo."Book" ("BookId", "Name") values ('4b976974-a673-499d-aca8-9ed6eeeed2fc', 'Chef');
insert into dbo."Book" ("BookId", "Name") values ('0c781bca-31a3-4925-a837-534e2fca3859', 'Living Desert, The');
insert into dbo."Book" ("BookId", "Name") values ('2cb00d37-5a8c-49d6-aceb-c006f4f3c061', 'General''s Daughter, The');
insert into dbo."Book" ("BookId", "Name") values ('389ef1f5-e2b0-4185-8185-47cd8bbfdb23', 'A Bright Shining Lie');
insert into dbo."Book" ("BookId", "Name") values ('8045c93f-7a06-464c-ae32-a18bf14d6f6c', 'House of 1000 Corpses');
insert into dbo."Book" ("BookId", "Name") values ('e19c728b-73a6-4665-a209-45fa614e3f47', 'Lumberjacking');
insert into dbo."Book" ("BookId", "Name") values ('9e53df56-09bd-46e7-b875-64c50c2606ed', 'I Served the King of England');
insert into dbo."Book" ("BookId", "Name") values ('58246182-4772-4c8d-893c-b5e3c8f6f773', 'Gunfighters');
insert into dbo."Book" ("BookId", "Name") values ('664e3559-73b6-48fa-b69d-ad39cf14d7e6', 'Bank Error in Your Favour');
--rollback TRUNCATE TABLE dbo.Book;