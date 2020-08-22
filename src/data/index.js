import { Sequelize } from 'sequelize';
import logger from '../util/logger';

//TODO: make username/password process.env
const sequelize = new Sequelize('postgres', 'postgres', 'password', {
  dialect: 'postgres',
  host: 'localhost',
  logging: false,
  define: {
    freezeTableName: true,
    schema: 'public',
  },
  dialectOptions: {
    options: {
      requestTimeout: 3000,
    },
  },
});

export function connectDatabase() {
  return new Promise(async (resolve, reject) => {
    try {
      const res = await sequelize.authenticate();
      logger.info('Database Connected');
      resolve(res);
    } catch (error) {
      reject(error);
    }
  });
}

export default sequelize;
