export interface SaveFile {
    profile: Profile;
    map: Map;
}

export interface Map {
    xRange: number;
    yRange: number;
    grid: cells[];
}

export interface cells {
    x: number;
    y: number;
    objType: number;
    ObjInfo: ObjInfo;
}

export interface ObjInfo {
    exitPoints?: boolean;
    powered?: boolean;
    dataStored: number;
    powerStored: number;
    level: number;
    age: number;
    upkeepCost: number;
    dataMined: number;
    dataSold: number;
    dataTransferd: number;
    Prio?: number[];
    SelfPrio?: number;
    updateSpeed?: number;
}

export interface Profile {
    Name: string;
    DateMade: string;
    DateSeen: string;
    TimePlayed: string;
    Statistics: Statistics;
}

export interface Statistics {
    networth: number;
    money: number;
    data: number;
    xp: number;
    Level: number;
}

// Converts JSON strings to/from your types
// and asserts the results of JSON.parse at runtime
export class Convert {
    public static toSaveFile(json: string): SaveFile {
        return cast(JSON.parse(json), r("SaveFile"));
    }

    public static saveFileToJson(value: SaveFile): string {
        return JSON.stringify(uncast(value, r("SaveFile")), null, 2);
    }
}

function invalidValue(typ: any, val: any, key: any = ""): never {
    if (key) {
        throw Error(
            `Invalid value for key "${key}". Expected type ${JSON.stringify(
                typ
            )} but got ${JSON.stringify(val)}`
        );
    }
    throw Error(
        `Invalid value ${JSON.stringify(val)} for type ${JSON.stringify(typ)}`
    );
}

function jsonToJSProps(typ: any): any {
    if (typ.jsonToJS === undefined) {
        const map: any = {};
        typ.props.forEach(
            (p: any) => (map[p.json] = { key: p.js, typ: p.typ })
        );
        typ.jsonToJS = map;
    }
    return typ.jsonToJS;
}

function jsToJSONProps(typ: any): any {
    if (typ.jsToJSON === undefined) {
        const map: any = {};
        typ.props.forEach(
            (p: any) => (map[p.js] = { key: p.json, typ: p.typ })
        );
        typ.jsToJSON = map;
    }
    return typ.jsToJSON;
}

function transform(val: any, typ: any, getProps: any, key: any = ""): any {
    function transformPrimitive(typ: string, val: any): any {
        if (typeof typ === typeof val) return val;
        return invalidValue(typ, val, key);
    }

    function transformUnion(typs: any[], val: any): any {
        // val must validate against one typ in typs
        const l = typs.length;
        for (let i = 0; i < l; i++) {
            const typ = typs[i];
            try {
                return transform(val, typ, getProps);
            } catch (_) {}
        }
        return invalidValue(typs, val);
    }

    function transformEnum(cases: string[], val: any): any {
        if (cases.indexOf(val) !== -1) return val;
        return invalidValue(cases, val);
    }

    function transformArray(typ: any, val: any): any {
        // val must be an array with no invalid elements
        if (!Array.isArray(val)) return invalidValue("array", val);
        return val.map((el) => transform(el, typ, getProps));
    }

    function transformDate(val: any): any {
        if (val === null) {
            return null;
        }
        const d = new Date(val);
        if (isNaN(d.valueOf())) {
            return invalidValue("Date", val);
        }
        return d;
    }

    function transformObject(
        props: { [k: string]: any },
        additional: any,
        val: any
    ): any {
        if (val === null || typeof val !== "object" || Array.isArray(val)) {
            return invalidValue("object", val);
        }
        const result: any = {};
        Object.getOwnPropertyNames(props).forEach((key) => {
            const prop = props[key];
            const v = Object.prototype.hasOwnProperty.call(val, key)
                ? val[key]
                : undefined;
            result[prop.key] = transform(v, prop.typ, getProps, prop.key);
        });
        Object.getOwnPropertyNames(val).forEach((key) => {
            if (!Object.prototype.hasOwnProperty.call(props, key)) {
                result[key] = transform(val[key], additional, getProps, key);
            }
        });
        return result;
    }

    if (typ === "any") return val;
    if (typ === null) {
        if (val === null) return val;
        return invalidValue(typ, val);
    }
    if (typ === false) return invalidValue(typ, val);
    while (typeof typ === "object" && typ.ref !== undefined) {
        typ = typeMap[typ.ref];
    }
    if (Array.isArray(typ)) return transformEnum(typ, val);
    if (typeof typ === "object") {
        return typ.hasOwnProperty("unionMembers")
            ? transformUnion(typ.unionMembers, val)
            : typ.hasOwnProperty("arrayItems")
            ? transformArray(typ.arrayItems, val)
            : typ.hasOwnProperty("props")
            ? transformObject(getProps(typ), typ.additional, val)
            : invalidValue(typ, val);
    }
    // Numbers can be parsed by Date but shouldn't be.
    if (typ === Date && typeof val !== "number") return transformDate(val);
    return transformPrimitive(typ, val);
}

function cast<T>(val: any, typ: any): T {
    return transform(val, typ, jsonToJSProps);
}

function uncast<T>(val: T, typ: any): any {
    return transform(val, typ, jsToJSONProps);
}

function a(typ: any) {
    return { arrayItems: typ };
}

function u(...typs: any[]) {
    return { unionMembers: typs };
}

function o(props: any[], additional: any) {
    return { props, additional };
}

function m(additional: any) {
    return { props: [], additional };
}

function r(name: string) {
    return { ref: name };
}

const typeMap: any = {
    SaveFile: o(
        [
            { json: "profile", js: "profile", typ: r("Profile") },
            { json: "map", js: "map", typ: r("Map") },
        ],
        false
    ),
    Map: o(
        [
            { json: "xRange", js: "xRange", typ: 3.14 },
            { json: "yRange", js: "yRange", typ: 3.14 },
            { json: "grid", js: "grid", typ: a(r("Grid")) },
        ],
        false
    ),
    Grid: o(
        [
            { json: "x", js: "x", typ: 0 },
            { json: "y", js: "y", typ: 0 },
            { json: "objType", js: "objType", typ: 0 },
            { json: "ObjInfo", js: "ObjInfo", typ: r("ObjInfo") },
        ],
        false
    ),
    ObjInfo: o(
        [
            { json: "exitPoints", js: "exitPoints", typ: u(undefined, true) },
            { json: "powered", js: "powered", typ: u(undefined, true) },
            { json: "dataStored", js: "dataStored", typ: 0 },
            { json: "powerStored", js: "powerStored", typ: 3.14 },
            { json: "level", js: "level", typ: 0 },
            { json: "age", js: "age", typ: 0 },
            { json: "upkeepCost", js: "upkeepCost", typ: 0 },
            { json: "dataMined", js: "dataMined", typ: 0 },
            { json: "dataSold", js: "dataSold", typ: 0 },
            { json: "dataTransferd", js: "dataTransferd", typ: 0 },
            { json: "Prio", js: "Prio", typ: u(undefined, a(0)) },
            { json: "SelfPrio", js: "SelfPrio", typ: u(undefined, 0) },
            { json: "updateSpeed", js: "updateSpeed", typ: u(undefined, 0) },
        ],
        false
    ),
    Profile: o(
        [
            { json: "Name", js: "Name", typ: "" },
            { json: "DateMade", js: "DateMade", typ: "" },
            { json: "DateSeen", js: "DateSeen", typ: "" },
            { json: "TimePlayed", js: "TimePlayed", typ: "" },
            { json: "Statistics", js: "Statistics", typ: r("Statistics") },
        ],
        false
    ),
    Statistics: o(
        [
            { json: "networth", js: "networth", typ: 0 },
            { json: "money", js: "money", typ: 0 },
            { json: "data", js: "data", typ: 0 },
            { json: "xp", js: "xp", typ: 0 },
            { json: "Level", js: "Level", typ: 0 },
        ],
        false
    ),
};
