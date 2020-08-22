import { DataTypes, Model } from 'sequelize';
import sequelize from './index';

class Article extends Model {}

Article.init(
  {
    // Model attributes are defined here
    title: {
      type: DataTypes.STRING,
      allowNull: false,
    },
    description: {
      type: DataTypes.STRING,
      allowNull: false,
    },
  },
  {
    // Other model options go here
    sequelize,
    modelName: 'Article',
  }
);

Article.sync({ force: true });

export default Article;
