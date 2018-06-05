package controllers

import (
	"encoding/json"
	"html/template"
	"net/http"
	"time"
	"bmwadforth/models"
)

var tpl *template.Template

func init() {
	tpl = template.Must(template.ParseGlob("views/*.gohtml"))
}

func Index(w http.ResponseWriter, r *http.Request) {
	w.Header().Add("content-type", "text/html")
	tpl.ExecuteTemplate(w, "index.gohtml", 42)
}

func Test(w http.ResponseWriter, r *http.Request){
	timeCookie := time.Now();
	timeCookie.Add(5000)
	cookie := http.Cookie{"testCookie", "testValue", "/", "localhost", timeCookie, "", 1, true, false, "", nil}
	http.SetCookie(w, &cookie)
	json.NewEncoder(w).Encode(models.HttpRepsonse{200, false, "Successfully set the cookie"})
}

func AuthToken(w http.ResponseWriter, r *http.Request) {
	w.Header().Add("content-type", "application/json")
	json.NewEncoder(w).Encode(r)
}
