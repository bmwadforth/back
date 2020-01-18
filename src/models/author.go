package models

import "time"

type Author struct {
	ID        int       `json:"id"`
	Username  string    `json:"username"`
	Password  []byte    `json:"-"`
	FirstName string    `json:"firstName"`
	LastName  string    `json:"lastName"`
	Created   time.Time `json:"created"`
}
