package controllers

import (
	"bmwadforth/common"
	"bmwadforth/util"
	"bmwadforth/models"
	"encoding/json"
	"net/http"
	"fmt"
	"time"
)

func NewUser(w http.ResponseWriter, r *http.Request) {
	fmt.Println("# Inserting bmwadforth.users")

	var temp models.User

	decoder := json.NewDecoder(r.Body)
	err := decoder.Decode(&temp)
	util.CheckErr(err)
	defer r.Body.Close()

	plainTextPw := temp.Password
	hashedPassword := common.GeneratePasswordHash([]byte(plainTextPw))

	db := common.GetDB()
	var lastInsertId int
	sqlStmt := `INSERT INTO bmwadforth.users (username, password) VALUES ($1, $2) RETURNING ID;`
	err = db.QueryRow(sqlStmt, temp.Username, hashedPassword).Scan(&lastInsertId)
	util.CheckErr(err)

	res := models.HttpRepsonse{Status: 200, Error: false, Message: "Successfully created user", Data: lastInsertId}
	data, _ := json.Marshal(res)

	w.Header().Add("content-type", "application/json")
	w.Write(data)
}

func UserLogin(w http.ResponseWriter, r *http.Request) {
	w.Header().Add("access-control-allow-origin", "http://localhost:3000")
	fmt.Println("# Querying bmwadforth.users")

	var temp models.User

	decoder := json.NewDecoder(r.Body)
	err := decoder.Decode(&temp)
	util.CheckErr(err)
	defer r.Body.Close()

	email := temp.Username
	plainPassword := temp.Password
	w.Header().Add("content-type", "application/json")
	if common.ComparePasswordHash(email, plainPassword) {
		timeCookie := time.Now()
		timeCookie.Add(5000)
		cookie := http.Cookie{"testCookie", "testValue", "/", "localhost", timeCookie, "", 1, true, false, "", nil}
		http.SetCookie(w, &cookie)
		json.NewEncoder(w).Encode(models.HttpRepsonse{200, false, "Successfully set the cookie", ""})
	} else {
		w.WriteHeader(http.StatusUnauthorized)
		json.NewEncoder(w).Encode(models.HttpRepsonse{401, false, "Unauthorised", ""})
	}
}
