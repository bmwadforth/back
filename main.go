package main

import (
	"bmwadforth/controllers"
	"github.com/gin-gonic/gin"
	"log"
)

func main() {
	r := gin.Default()
	r.GET("/ping",  controllers.Ping)
	api := r.Group("/api")
	api.GET("/articles", controllers.GetArticles)
	api.GET("/article/:id", controllers.GetArticle)
	api.POST("/article", controllers.NewArticle)

	err := r.Run()
	if err != nil {
		log.Fatal(err)
	}
}