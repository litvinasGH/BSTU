import { Sequelize } from "sequelize";

const Model = Sequelize.Model;

class Faculty extends Model {}
class Pulpit extends Model {}
class Teacher extends Model {}
class Subject extends Model {}
class Auditorium_type extends Model {}
class Auditorium extends Model {}

function Initialize(sequelize) {

    Faculty.init({
        FACULTY: {
            type: Sequelize.STRING,
            primaryKey: true
        },
        FACULTY_NAME: Sequelize.STRING
    },{
        sequelize,
        modelName: 'Faculty',
        tableName: 'FACULTY',
        timestamps: false
    });


    Pulpit.init({
        PULPIT: {
            type: Sequelize.STRING,
            primaryKey: true
        },
        PULPIT_NAME: Sequelize.STRING,
        FACULTY: {
            type: Sequelize.STRING
        }
    },{
        sequelize,
        modelName: 'Pulpit',
        tableName: 'PULPIT',
        timestamps: false
    });


    Teacher.init({
        TEACHER: {
            type: Sequelize.STRING,
            primaryKey: true
        },
        TEACHER_NAME: Sequelize.STRING,
        PULPIT: Sequelize.STRING
    },{
        sequelize,
        modelName: 'Teacher',
        tableName: 'TEACHER',
        timestamps: false
    });


Subject.init({
    SUBJECT: {
        type: Sequelize.STRING(10),
        primaryKey: true
    },
    SUBJECT_NAME: {
        type: Sequelize.STRING(100)
    },
    PULPIT: {
        type: Sequelize.STRING(20)
    }
},{
    sequelize,
    tableName: 'SUBJECT',
    timestamps: false
});


    Auditorium_type.init({
        AUDITORIUM_TYPE: {
            type: Sequelize.STRING,
            primaryKey: true
        },
        AUDITORIUM_TYPENAME: Sequelize.STRING
    },{
        sequelize,
        modelName: 'Auditorium_type',
        tableName: 'AUDITORIUM_TYPE',
        timestamps: false
    });


    Auditorium.init({
        AUDITORIUM: {
            type: Sequelize.STRING,
            primaryKey: true
        },
        AUDITORIUM_NAME: Sequelize.STRING,
        AUDITORIUM_CAPACITY: Sequelize.INTEGER,
        AUDITORIUM_TYPE: Sequelize.STRING
    },{
        sequelize,
        modelName: 'Auditorium',
        tableName: 'AUDITORIUM',
        timestamps: false
    });

    // -----------------------
    // ASSOCIATIONS
    // -----------------------

    Faculty.hasMany(Pulpit, {
        foreignKey: 'FACULTY',
        as: 'pulpits'
    });

    Pulpit.belongsTo(Faculty,{
        foreignKey: 'FACULTY',
        as: 'faculty'
    });


    Pulpit.hasMany(Subject,{
        foreignKey: 'PULPIT',
        as: 'subjects'
    });

    Subject.belongsTo(Pulpit,{
        foreignKey: 'PULPIT',
        as: 'pulpit'
    });


    Pulpit.hasMany(Teacher,{
        foreignKey: 'PULPIT',
        as: 'teachers'
    });

    Teacher.belongsTo(Pulpit,{
        foreignKey: 'PULPIT',
        as: 'pulpit'
    });


    Auditorium_type.hasMany(Auditorium,{
        foreignKey: 'AUDITORIUM_TYPE',
        as: 'auditoriums'
    });

    Auditorium.belongsTo(Auditorium_type,{
        foreignKey: 'AUDITORIUM_TYPE',
        as: 'type'
    });

}

export function ORM(sequelize){
    Initialize(sequelize);

    return {
        Faculty,
        Pulpit,
        Teacher,
        Subject,
        Auditorium_type,
        Auditorium
    };
}