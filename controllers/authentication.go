package controllers

import (
	"bmwadforth/common"
	models "bmwadforth/models"
	"encoding/json"
	"log"
	"net/http"
)

func NewUser(w http.ResponseWriter, r *http.Request) {
	error := r.ParseForm()
	if error != nil {
		log.Fatal(error)
	}

	formData := make(map[string]string)

	for key, _ := range r.Form {
		formData[key] = r.Form.Get(key)
	}

	plainTextPw := formData["password"]
	hashedPassword := common.GeneratePasswordHash([]byte(plainTextPw))

	db := common.GetDB()
	sqlStatement := `INSERT INTO xmanage.users (first_name, last_name, email, password) VALUES ($1, $2, $3, $4)`
	_, error = db.Exec(sqlStatement, formData["first_name"], formData["last_name"], formData["email"], hashedPassword)
	if error != nil {
		panic(error)
	}
}

func UserLogin(w http.ResponseWriter, r *http.Request) {
	error := r.ParseForm()
	checkErr(error)

	formData := make(map[string]string)

	for key, _ := range r.Form {
		formData[key] = r.Form.Get(key)
	}

	email := formData["email"]
	plainPassword := formData["password"]
	w.Header().Add("content-type", "application/json")
	if common.ComparePasswordHash(email, plainPassword) {
		w.WriteHeader(http.StatusOK)
		json.NewEncoder(w).Encode(models.HttpRepsonse{200, false, "Authorized"})
	} else {
		w.WriteHeader(http.StatusUnauthorized)
		json.NewEncoder(w).Encode(models.HttpRepsonse{401, false, "Unauthorised"})
	}
}

func checkErr(err error) {
	if err != nil {
		panic(err)
	}
}
