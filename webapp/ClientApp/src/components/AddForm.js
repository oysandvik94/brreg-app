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
import Snackbar from "@material-ui/core/Snackbar";
import Alert from '@material-ui/lab/Alert';
import {OrganizationTable} from "./OrganizationsTable";
import * as PropTypes from "prop-types";

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

export default function AddForm(props) {
    const classes = useStyles();
    const [open, setOpen] = React.useState(false);
    const [inputValue, setValue] = React.useState('');
    const [selectedValue, setSelectedValue] = React.useState(null);
    const [statusMessage, setStatusMessage] = React.useState('');
    const [showSnackbar, setShowSnackbar] = React.useState(false);
    const [snackType, setSnackType] = React.useState('');
    // Form values
    const [formOrgName, setFormOrgName] = React.useState('');
    const [formOrgnr, setFormOrgnr] = React.useState('');
    const [formOrgType, setFormOrgType] = React.useState('');
    const [formMunicipality, setFormMunicipality] = React.useState('');
    const [customNote, setCustomNote] = React.useState('');
    
    
    // handle input change event
    const handleInputChange = value => {
        setValue(value);
    };

    /**
     * Handles brreg search selection. 
     * Set value and update form
     * @param value JSON res from brreg query
     */
    const handleChange = value => {
        setSelectedValue(value);
        setFormOrgName(value["navn"]);
        setFormOrgnr(value["organisasjonsnummer"]);
        setFormOrgType(value["organisasjonsform"]["beskrivelse"]);
        setFormMunicipality(value["forretningsadresse"]["kommune"]);
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

    const handleCloseSnackbar = (event, reason) => {
        setShowSnackbar(false);
    };
    
    const handleCreateStatus = (status) => {
        if (status === 201) {
            setStatusMessage("Ny organisasjon opprettet");
            setSnackType("success");
        } else {
            setStatusMessage("Kunne ikke opprette ny organisasjon");
            setSnackType("error");
        }
        setShowSnackbar(true);
        props.onSave();
    }

   function handleTextFieldChange(e) {
        setCustomNote(e.target.value);
    }

    const saveOrg = () => {
        setOpen(false);
        
        if (!formOrgnr) {
            return;
        }
        
        // Map form to organization model
        let organization={
            orgname:formOrgName,
            orgnr:formOrgnr,
            municipality:formMunicipality,
            orgtype:formOrgType,
            note:customNote
        };
        
        fetch('organizations',{
            method: 'POST',
            headers:{'Content-type':'application/json-patch+json'},
            body: JSON.stringify(organization)
        }).then(r =>
            handleCreateStatus(r.status)
        );
    };

    return (
        <div>
            <Snackbar open={showSnackbar} autoHideDuration={2000} onClose={handleCloseSnackbar}>
                <Alert severity={snackType}>{statusMessage}</Alert>
            </Snackbar>
            
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
                        <Container className={classes.formContainer} >
                            <TextField
                                id="orgname"
                                label="Navn"
                                value={formOrgName}
                                onChange={(e) => setFormOrgName(e.target.value)}
                            />
                            <TextField
                                id="orgnr"
                                label="Organisasjonsnummer"
                                value={formOrgnr}
                                onChange={(e) => setFormOrgnr(e.target.value)}
                            />
                            <TextField
                                id="orgtype"
                                label="Organisasjonform"
                                value={formOrgType}
                                onChange={(e) => setFormOrgType(e.target.value)}
                            />
                            <TextField
                                id="municipality"
                                label="Kommune"
                                value={formMunicipality}
                                onChange={(e) => setFormMunicipality(e.target.value)}
                            />
                            <TextField
                                id="note"
                                label="Eget notat"
                                multiline={true}
                                fullWidth
                                variant="outlined"
                                value={customNote}
                                onChange={handleTextFieldChange}
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

AddForm.propTypes = {
    onSave: PropTypes.func
};
