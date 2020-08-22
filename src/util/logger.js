import winston from 'winston';

const console = winston.format.printf(({ level, message, timestamp }) => {
  const logLevel = winston.format.colorize().colorize(level, `${level.toUpperCase()}`);
  return `${timestamp} [${logLevel}] ${message}`;
});

const logger = winston.createLogger({
  transports: [
    new winston.transports.Console({
      level: process.env.LOG_LEVEL || 'info',
      format: winston.format.combine(winston.format.timestamp(), console),
    }),
  ],
});

export default logger;
