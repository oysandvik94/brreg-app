import React from 'react';
import Button from '@material-ui/core/Button';
import TextField from '@material-ui/core/TextField';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogTitle from '@material-ui/core/DialogTitle';
import AsyncSelect from 'react-select/async';
import {makeStyles} from "@material-ui/core/styles";
import Container from "@material-ui/core/Container";
import grey from "@material-ui/core/colors/grey";

const useStyles = makeStyles({
    dialog: {
        height: 800,
        width: 600
    },
    root: {
        '& .MuiTextField-root': {
            margin: 2,
            marginTop: 20
        },
    },
    formContainer: {
        marginTop: 12
    }
});

export default function AddForm() {
    const classes = useStyles();
    const [open, setOpen] = React.useState(false);
    const [inputValue, setValue] = React.useState('');
    const [selectedValue, setSelectedValue] = React.useState(null);
    
    // handle input change event
    const handleInputChange = value => {
        setValue(value);
    };

    // handle selection
    const handleChange = value => {
        setSelectedValue(value);
    }

    // load options using API call
    const loadOptions = (inputValue) => {
        return fetch(`QueryBrreg/${inputValue}`).then(res => res.json());
    };

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    const saveOrg = () => {
        setOpen(false);
        // TODO: Lagre
    };

    return (
        <div>
            <Button variant="contained" color="secondary" onClick={handleClickOpen}>
                Søk og lagre
            </Button>
            <Dialog open={open} onClose={handleClose} aria-labelledby="form-dialog-title">
                <DialogTitle id="form-dialog-title">Ny organisasjon</DialogTitle>
                <DialogContent className={classes.dialog}>
                    <AsyncSelect
                        cacheOptions
                        defaultOptions
                        value={selectedValue}
                        getOptionLabel={e => e.navn}
                        getOptionValue={e => e.organisasjonsnummer}
                        loadOptions={loadOptions}
                        onInputChange={handleInputChange}
                        onChange={handleChange}
                        placeholder={"Søk i Brønnøysregisteret"}
                        styles={{
                            // Fixes the overlapping problem of the component
                            menu: provided => ({ ...provided, zIndex: 9999 })
                        }}
                    />
                    <form className={classes.root} noValidate autoComplete="off">
                        <Container className={classes.formContainer} hidden={selectedValue === null}>
                            <TextField
                                id="orgname"
                                label="Navn"
                                value={selectedValue !== null ? selectedValue["navn"] : ''}
                                aria-readonly={true}
                            />
                            <TextField
                                id="orgnr"
                                label="Organisasjonsnummer"
                                value={selectedValue !== null ? selectedValue["organisasjonsnummer"] : ''}
                                aria-readonly={true}
                            />
                            <TextField
                                id="orgtype"
                                label="Organisasjonform"
                                aria-readonly={true}
                                value={selectedValue !== null ? selectedValue["organisasjonsform"]["beskrivelse"] : ''}
                            />
                            <TextField
                                id="municipality"
                                label="Kommune"
                                value={selectedValue !== null ? selectedValue["forretningsadresse"]["kommune"] : ''}
                                aria-readonly={"true"}
                            />
                            <TextField
                                id="note"
                                label="Eget notat"
                                multiline={true}
                                fullWidth
                                variant="outlined"
                            />
                        </Container>
                    </form>
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleClose} color="primary">
                        Avbryt
                    </Button>
                    <Button onClick={saveOrg} color="primary">
                        Lagre
                    </Button>
                </DialogActions>
            </Dialog>
        </div>
    );
}

async function fetchData(data) {
    // Avoid doing unnecessary searches with just a few letters
    if (data !== undefined || data.length < 3) {
        return {};
    }
    
/*    const response = await fetch('QueryBrreg/' + data);
    return await response.json();*/
}