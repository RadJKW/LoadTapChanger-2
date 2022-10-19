/*markdown
# LoadTapChanger DB Notebook
*/

-- select all the micrologix tags on plc 1
select * from MicrologixTag where PlcId = 1;

/*markdown
## Lets create a lookup table for TagType
*/

drop table Tag_Type;

-- create a Tag_Type table Enum for the tag types


create table if not exists Tag_Type (
    id integer primary key,
    tagType varchar(20) not null UNIQUE
);


insert into Tag_Type (id, tagType) values
(0, 'Output'),
(1, 'Input'),
(2, 'Status'),
(3, 'Binary'),
(4,'Timer'),
(5,'Counter'),
(6, 'Control'),
(7, 'Integer'),
(8, 'Float'),
(9, 'Custom9'),
(10,'Custom10'),
(11,'Custom11'),
(12,'Custom12');


select * from Tag_Type;

/*markdown
Lets alter the table to add a foreign key for TagType to Tag_Type
*/

-- alter table MicrologixTag to add the constraint fk_Tag_Type (tagType) references Tag_Type(id)
