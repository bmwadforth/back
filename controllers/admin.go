package controllers

import (
	"bmwadforth/util"
	"github.com/gin-gonic/gin"
	"net/http"
)

func LoginAdmin(c *gin.Context) {
	response := util.NewResponse(http.StatusNotImplemented, "Login Not Implemented")
	c.JSON(response.Status, response)
}