package util

import (
	"net/http"
	"time"
)

func AddCookie(w http.ResponseWriter, name string, value string, domain string) {
	expire := time.Now().AddDate(0, 0, 1)
	cookie := http.Cookie{
		Name:    name,
		Value:   value,
		Expires: expire,
		Domain: domain,
	}
	http.SetCookie(w, &cookie)
}