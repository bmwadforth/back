package common

import (
	"golang.org/x/crypto/bcrypt"
	"log"
)

func GeneratePasswordHash(password []byte) string {
	hash, error := bcrypt.GenerateFromPassword(password, bcrypt.MinCost)
	if error != nil {
		panic(error)
	}
	return string(hash)
}

func ComparePasswordHash(username string, plainPassword string) bool {
	db := GetDB()

	rows, err := db.Query(`SELECT password FROM bmwadforth.users WHERE username = $1`, username)
	if err != nil {
		log.Fatal(err)
	}

	defer rows.Close()
	var hashPassword string
	for rows.Next() {
		err = rows.Scan(&hashPassword)
		checkErr(err)
	}

	byteHash := []byte(hashPassword)
	bytePlain := []byte(plainPassword)

	error := bcrypt.CompareHashAndPassword(byteHash, bytePlain)
	if error != nil {
		return false
	}

	return true
}

func checkErr(err error) {
	if err != nil {
		panic(err)
	}
}
