FROM rust

WORKDIR /usr/src/bmwadforth
COPY . .

RUN cargo install --path .

CMD ["bmwadforth"]