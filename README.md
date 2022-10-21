# LoadTapChanger-2

## Overview
<!--<details>
    <summary>
    REQUIREMENTS
    </summary>-->

<!--body shown when uncollapsed-->

**MINIMAL**

_Necessary Requirements to proceed with *Development*_

- [ ] **PLC**: *Data Manipulation*
 
    > *_GET_*
    > - List All Plcs
    > - Lookup Details of a single PLC
    > - List ALl the Tags assigned to the PLC
    > - Verify Plc Exists
    >
    > *_POST_*
    > - Create a new Plc
    >
    > *_PUT_*
    > - Update the configuration of existing PLC
    >
    > *_DELETE_*
    > - Delete a Plc =>  `DON'T Delete tags, set{PlcTags.PlcDeviceId = Null}`

- [ ] **Tag**: *Data Manipulation*

    > *_GET_*
    > - List All The Tags
    > - Lookup Details of a single Tag
    > - Verify Tag Exists
    >
    > *_POST_*
    > - Create a new Tag w/o  PLC => `Assing {PlcDeviceId = NULL}`
    > - Create a new Tag w/ PLC => `Assign {PlcDeviceId = [SelectedDevice]}
    >
    > *_PUT_*
    > - Update the configuration of existing Tag
    >
    > *_DELETE_*
    > - Delete a TAG
<!--</details>-->

## TODO

### Finalize Database





## WebAPI
<details>
<summary>
    Layout
</summary>

- [ ] **HTTP METHODS**
    > - [ ] GetAll
    > - [ ] GetById
    > - [ ] Put
    > - [ ] Post

- [ ] **CRUD OPERATIONS**
    > - [ ] CreatePlc
    > - [ ] ReadPlc
    > - [ ] ReadPlcs
    > - [ ] ReadDetailsPlc
    > - [ ] UpdatePlc
    > - [ ] DeletePlc

- [ ] **Data Transfer Objects**
    > - [ ] PlcDetailsDto
    > - [ ] PlcCreateDto
    > - [ ] PlcReadDto
    > - [ ] PlcUpdateDto

- [ ] **Repositories
    > - [ ] 
</details>

<details>
    <summary>
    Plc Tags
    </summary>

> CRUD OPERATIONS
> - [ ] CreateTag
> - [ ] 
> - [ ]
> - [ ]
> - [ ]
> - [ ]
> - [ ]
> - [ ]
</details>

**FEATURE REQUESTS**

<sup>These are from the view point of the `Client`.
(ie: What information should the **CLIENT** be able to request)</sup>

- [ ] #01: New Feature Format
