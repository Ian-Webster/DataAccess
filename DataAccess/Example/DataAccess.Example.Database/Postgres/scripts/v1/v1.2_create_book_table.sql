--liquibase formatted sql

/*
 * Create the Book table
 *
 * Author: Webster
 * Date: 2022-12-03
 */

-- changeset webster:1.1
-- comment: create book table
CREATE TABLE dbo."Book"(
	"BookId" uuid NOT NULL,
	"Name" varchar(128) NOT NULL,
 CONSTRAINT book_pk PRIMARY KEY ("BookId")
);
-- rollback DROP TABLE dbo.book