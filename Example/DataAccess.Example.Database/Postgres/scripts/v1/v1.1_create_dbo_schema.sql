--liquibase formatted sql

/*
 * Create the dbo schema
 *
 * Author: Webster
 * Date: 2022-12-03
 */

-- changeset webster:1.1
-- comment: create dbo schema
CREATE SCHEMA dbo AUTHORIZATION postgres
-- rollback DROP SCHEMA common