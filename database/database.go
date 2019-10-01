package database

import (
	"database/sql"
	"fmt"
	_ "github.com/lib/pq"
	"log"
	"os"
)

type Credentials struct {
	Username string
	Password string
	Database string
	Host     string
	Port     string
}

type Instance struct {
	Database *sql.DB
}

func LoadCredentials() Credentials {
	return Credentials{
		Username: os.Getenv("BMWADFORTH_USERNAME"),
		Password: os.Getenv("BMWADFORTH_PASSWORD"),
		Database: os.Getenv("BMWADFORTH_DATABASE"),
		Host:     os.Getenv("BMWADFORTH_HOST"),
		Port:     os.Getenv("BMWADFORTH_PORT"),
	}
}

func (c *Credentials) ConnectionString() string {
	return fmt.Sprintf("postgres://%s:%s@%s/%s?sslmode=disable", c.Username, c.Password, c.Host, c.Database)
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
