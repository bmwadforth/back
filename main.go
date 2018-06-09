package main

import (
	common "bmwadforth/common"
	controllers "bmwadforth/controllers"
	enums "bmwadforth/enums"
	"fmt"
	"log"
	"net/http"
	"time"

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
	r.HandleFunc("/api/articles/add", controllers.NewArticle).Methods("POST")

	return r
}

func main() {
	db := common.DBConnect()
	defer db.Close()

	r := defineRoutes()

	log.Println("Starting webserver")

	go func() {
		for {
			time.Sleep(time.Second)

			log.Println("Checking if started...")
			resp, err := http.Get("http://localhost:8000")
			if err != nil {
				log.Println("Failed:", err)
				continue
			}
			resp.Body.Close()
			if resp.StatusCode != http.StatusOK {
				log.Println("Not OK:", resp.StatusCode)
				continue
			}

			// Reached this point: server is up and running!
			break
		}
		log.Println("SERVER UP AND RUNNING")
	}()

	portListen := fmt.Sprintf(":%d", enums.WEB_Port)
	log.Fatal(http.ListenAndServe(portListen, r))
}
