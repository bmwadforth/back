package database

import (
	"database/sql"
	"fmt"
	"log"
)

type Credentials struct {
	Username string
	Password string
	Database string
	Host     string
	Port     uint32
}

type Instance struct {
	Database *sql.DB
}

func LoadCredentials() Credentials {
	//TODO: Get credentials from the process environment/arguments
	return Credentials{
		Username: "postgres",
		Password: "password",
		Database: "postgres",
		Host:     "localhost",
		Port:     5432,
	}
}

func (c *Credentials) ConnectionString() string {
	return fmt.Sprintf("postgres://%s:%s@%s/%s?sslmode=verify-full", c.Username, c.Password, c.Host, c.Database)
}

func NewDatabaseConnection() *Instance {
	creds := LoadCredentials()
	db, err := sql.Open("postgres", creds.ConnectionString())
	i := Instance{
		Database: db,
	}
	if err != nil {
		log.Fatal(err)
	}

	return &i
}
