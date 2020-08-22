import express from 'express'
import logger from 'morgan';
import cookieParser from 'cookie-parser';
import articleRouter from './src/routes/articles.js';

const app = express();

app.use(logger('dev'));
app.use(express.json());
app.use(express.urlencoded({extended: false}));
app.use(cookieParser());

app.use('/api/v1/articles', articleRouter)
//app.use('/api/v1', articleRouter);

const PORT = process.env.PORT || 8080
app.listen(PORT, () => {
    console.log(`App listening on: ${PORT}`)
    console.log('Press Ctrl+C to quit.')
});
