package database

import (
	"errors"
	"github.com/bmwadforth/back/src/models"
	"log"
)

func GetAuthor(username string) (models.Author, error) {
	author := models.Author{}
	db := OpenDatabase()

	rows, err := db.Database.Query("SELECT * FROM BLOG.AUTHORS WHERE username = $1;", username)
	if err != nil {
		log.Println(err)
		return author, errors.New("unable to get author")
	}

	for rows.Next() {
		err := rows.Scan(&author.ID, &author.FirstName, &author.LastName, &author.Username, &author.Password, &author.Created)
		if err != nil {
			log.Println(err)
			return author, errors.New("unable to load author")
		}
	}

	return author, nil
}

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
