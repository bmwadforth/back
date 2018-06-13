package common

import (
	"crypto/sha512"
	"encoding/json"
	"log"

	"golang.org/x/crypto/bcrypt"
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

func GenerateSecret(secretSetter string, secretChan chan []byte) {
	hash := sha512.New()
	hash.Write([]byte(secretSetter))
	secret := hash.Sum(nil)
	data, _ := json.Marshal(secret)
	secretChan <- data
}

func checkErr(err error) {
	if err != nil {
		panic(err)
	}
}
