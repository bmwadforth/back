package database

import (
	"errors"
	"log"
)

func NewAuthor(firstName string, lastName string, username string, password string) error {
	instance := OpenDatabase()
	db := instance.Database

	tx, err := db.Begin(); if err != nil {
		log.Println(err)
		return errors.New("unable to create database transaction")
	}

	_, err = tx.Exec("INSERT INTO BLOG.AUTHORS(first_name, last_name, username, password) VALUES ($1, $2, $3, $4)", firstName, lastName, username, password)
	if err != nil {
		log.Println(err)

		err = tx.Rollback(); if err != nil {
			log.Println(err)
			return errors.New("unable to rollback database action")
		}

		return errors.New("unable to execute database action")
	}

	err = tx.Commit(); if err != nil {
		log.Println(err)
		return errors.New("unable to commit database action")
	}

	return nil
}
