package controllers

import (
	"bmwadforth/database"
	"bmwadforth/util"
	"github.com/gin-gonic/gin"
	"net/http"
)

func GetArticles(c *gin.Context) {
	articles, err := database.GetArticles()
	if err != nil {
		response := util.NewResponse(http.StatusOK, "Articles Fetched Unsuccessful").AddError(err)
		c.JSON(response.Status, response)
		return
	}

	response := util.NewResponse(http.StatusOK, "Articles Fetched Successfully").SetData(articles)
	c.JSON(response.Status, response)
}

func GetArticle(c *gin.Context) {
	id := c.Param("id")
	article, err := database.GetArticle(id)
	if err != nil {
		response := util.NewResponse(http.StatusOK, "Article Fetch Unsuccessful").AddError(err)
		c.JSON(response.Status, response)
		return
	}

	response := util.NewResponse(http.StatusOK, "Article Fetched Successfully").SetData(article)
	c.JSON(response.Status, response)
}

func NewArticle(c *gin.Context) {
	c.JSON(200, gin.H{
		"message": "pong",
	})
}