package main

import (
	"bmwadforth/controllers"
	"log"
	"time"

	"github.com/gin-contrib/cors"
	"github.com/gin-gonic/gin"
)

func main() {
	r := gin.Default()

	r.Use(cors.New(cors.Config{
		AllowOrigins:     []string{"https://bmwadforth.com", "http://bmwadforth.com", "http://localhost:3000"},
		AllowMethods:     []string{"GET", "POST", "OPTIONS", "PUT", "PATCH", "DELETE"},
		AllowHeaders:     []string{"Origin", "Content-Type"},
		AllowCredentials: true,
		MaxAge:           24 * time.Hour,
	}))

	r.GET("/ping", controllers.Ping)
	api := r.Group("/api")
	api.GET("/articles", controllers.GetArticles)
	api.GET("/article/:id", controllers.GetArticle)
	api.POST("/article", controllers.NewArticle)

	err := r.Run()
	if err != nil {
		log.Fatal(err)
	}
}
