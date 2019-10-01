FROM golang:latest

RUN mkdir /go/src/bmwadforth
RUN go get -u github.com/golang/dep/cmd/dep
WORKDIR /go/src/bmwadforth

COPY . .

RUN dep ensure -v
RUN go build -o main .
EXPOSE 8080
CMD ["./main"]