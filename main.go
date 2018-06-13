package main

import (
	"bmwadforth/common"
	"bmwadforth/controllers"
	"bmwadforth/enums"
	"fmt"
	"log"
	"net/http"
	"time"

	"math/rand"

	"github.com/gorilla/mux"
)

func defineRoutes() *mux.Router {
	r := mux.NewRouter()

	r.HandleFunc("/", controllers.Index).Methods("GET")
	r.HandleFunc("/test", controllers.Test).Methods("GET")
	r.HandleFunc("/api/token", controllers.AuthToken).Methods("GET")
	r.HandleFunc("/api/register", controllers.NewUser).Methods("POST")
	r.HandleFunc("/api/login", controllers.UserLogin).Methods("POST")
	r.HandleFunc("/api/articles/all", controllers.GetArticles).Methods("GET")
	r.HandleFunc("/api/article", controllers.GetArticle).Methods("GET")
	r.HandleFunc("/api/articles/add", controllers.NewArticle).Methods("POST")

	r.HandleFunc("/api/secret/validate", ValidateSecret).Methods("POST")

	return r
}

var Secret string

func ValidateSecret(w http.ResponseWriter, r *http.Request) {
	secretAttemp := r.FormValue("secret")

	fmt.Println(Secret)
	if secretAttemp == Secret {
		w.Write([]byte("YES!"))
	} else {
		w.WriteHeader(http.StatusUnauthorized)
		w.Write([]byte("NO :("))
	}

}

func RandStringRunes(n int) string {
	var letterRunes = []rune("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ")
	b := make([]rune, n)
	for i := range b {
		b[i] = letterRunes[rand.Intn(len(letterRunes))]
	}
	return string(b)
}

func CheckServerUp() {
	resp, err := http.Get("http://localhost:8000")
	if err != nil {
		log.Fatal(err)
	}
	var msgBeenSent bool
	for resp.StatusCode != http.StatusOK {
		if !msgBeenSent {
			println("Unable to start server\n")
		}
		msgBeenSent = true
		println("Status code:", resp.StatusCode, "restarting server...")
		time.Sleep(3 * time.Second)
	}
	println("\nSERVER STARTED SUCCESSFULLY\n")
}

func GenerateNewSecret(secretChan chan []byte) {
	for range time.NewTicker(15 * time.Second).C {
		secret := RandStringRunes(12)
		go common.GenerateSecret(secret, secretChan)
		secretReceive := <-secretChan
		fmt.Println(string(secretReceive))
		Secret = string(secretReceive)
	}
}

func main() {
	db := common.DBConnect()
	defer db.Close()

	r := defineRoutes()

	log.Println("Starting webserver...\n")

	secretChan := make(chan []byte)

	go CheckServerUp()

	go GenerateNewSecret(secretChan)

	portListen := fmt.Sprintf(":%d", enums.WEB_Port)

	log.Fatal(http.ListenAndServe(portListen, r))

}
