package common

import (
	"golang.org/x/crypto/bcrypt"
)

func GeneratePasswordHash(password []byte) string {
	hash, error := bcrypt.GenerateFromPassword(password, bcrypt.MinCost)
	if error != nil {
		panic(error)
	}
	return string(hash)
}

func ComparePasswordHash(email string, plainPassword string) bool {
	db := GetDB()

	rows, err := db.Query(`SELECT password FROM xmanage.users WHERE email = $1`, email)
	if err != nil {
		panic(err)
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
